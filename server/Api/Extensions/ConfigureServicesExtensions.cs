using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Amazon.Runtime;
using Amazon.S3;
using CoffeeShop.Api.UserManagement;
using CoffeeShop.Api.Notifications;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.Common.Services;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.Services;
using CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;
using CoffeeShop.BusinessLogic.PaymentManagement.Services;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.BusinessLogic.ProductManagement.Services;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Services;
using CoffeeShop.DataAccess;
using CoffeeShop.DataAccess.Common.Repositories;
using CoffeeShop.DataAccess.OrderManagement.Repositories;
using CoffeeShop.DataAccess.PaymentManagement.Repositories;
using CoffeeShop.DataAccess.ProductManagement.Repositories;
using CoffeeShop.DataAccess.ProductManagement.Strategies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using ProductService = CoffeeShop.BusinessLogic.ProductManagement.Services.ProductService;
using TokenService = CoffeeShop.BusinessLogic.UserManagement.Services.TokenService;

namespace CoffeeShop.Api.Extensions;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection AddSharedServices(
        this IServiceCollection services,
        IWebHostEnvironment environment,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql(connectionString));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddCloudflareBlobStorage(configuration);

        services.AddHealthChecks();

        services
            .AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.DefaultIgnoreCondition =
                    JsonIgnoreCondition.WhenWritingNull;
            });

        if (environment.IsDevelopment())
        {
            services.AddCors(options =>
                options.AddDefaultPolicy(policyBuilder =>
                    policyBuilder
                        .SetIsOriginAllowed(origin => new Uri(origin).IsLoopback)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                )
            );
        }

        services.AddEndpointsApiExplorer();

        services.AddOpenApi();

        services.AddSignalR();

        return services;
    }

    public static IServiceCollection AddUserManagement(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddHttpContextAccessor();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMappingService, UserMappingService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        services.AddScoped<IUserAuthorizationService, UserAuthorizationService>();

        services
            .AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddRoleManager<RoleManager<IdentityRole>>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:SigningKey"]!)
                    ),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Cookies[Constants.AccessTokenName];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        var path = context.HttpContext.Request.Path;
                        var queryToken = context.Request.Query["access_token"];
                        if (path.StartsWithSegments("/hub/notifications") && !string.IsNullOrEmpty(queryToken))
                        {
                            context.Token = queryToken;
                        }
                        return Task.CompletedTask;
                    },
                };
            });

        return services;
    }

    public static IServiceCollection AddProductManagement(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMemoryCache, MemoryCache>();
        services.AddScoped<IProductMappingService, ProductMappingService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IProductImageService, ProductImageService>();
        services.AddProductSortingStrategy(configuration);
        services.AddProductRepository(configuration);

        return services;
    }

    private static void AddProductRepository(this IServiceCollection services, IConfiguration configuration)
    {
        var productRepositoryDecorator = configuration["Decorators:ProductRepository"];
        if (productRepositoryDecorator == nameof(CachedProductRepository))
        {
            services.AddScoped<IProductRepository>(serviceProvider =>
            {
                var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
                var cache = serviceProvider.GetRequiredService<IMemoryCache>();
                var logger = serviceProvider.GetRequiredService<ILogger<CachedProductRepository>>();
                var productSortingContext = serviceProvider.GetRequiredService<ProductSortingContext>();
            
                return new CachedProductRepository(
                    new ProductRepository(dbContext, productSortingContext),
                    cache,
                    logger);
            });
        }
        else
        {
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }

    private static void AddProductSortingStrategy(this IServiceCollection services, IConfiguration configuration)
    {
        var productSortingStrategy = configuration["Strategies:ProductSorting"];
        if (productSortingStrategy == nameof(ProductSortingByNameStrategy))
        {
            services.AddScoped<IProductSortingStrategy, ProductSortingByNameStrategy>();
        }
        else
        {
            services.AddScoped<IProductSortingStrategy, ProductSortingByPriceStrategy>();
        }
        
        services.AddScoped<ProductSortingContext>();
    }

    private static void AddCloudflareBlobStorage(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddSingleton<IBlobStorage, CloudflareBlobStorage>(_ =>
        {
            var accessKey = configuration["BlobStorage:AccessKey"]!;
            var secretKey = configuration["BlobStorage:SecretKey"]!;
            var accountId = configuration["BlobStorage:AccountId"]!;

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                ServiceURL = $"https://{accountId}.r2.cloudflarestorage.com",
                RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
                ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED,
            };

            var s3Client = new AmazonS3Client(credentials, config);

            return new CloudflareBlobStorage(s3Client, configuration);
        });
    }

    public static IServiceCollection AddOrderManagement(this IServiceCollection services)
    {
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderMappingService, OrderMappingService>();
        services.AddScoped<IOrderService, OrderService>();

        return services;
    }

    public static IServiceCollection AddPaymentManagement(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICardPaymentRepository, CardPaymentRepository>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IStripeService, StripeService>(_ => new StripeService(new PaymentIntentService()));

        StripeConfiguration.ApiKey = configuration["Stripe:ApiKey"];

        return services;
    }
    
    public static IServiceCollection AddNotifications(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrderNotificationService, OrderNotificationService>();

        return services;
    }
}
