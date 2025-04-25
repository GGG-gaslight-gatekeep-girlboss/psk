using System.Text.Json;
using System.Text.Json.Serialization;
using CoffeeShop.Application.UserManagement.Interfaces;
using CoffeeShop.Application.UserManagement.Services;
using CoffeeShop.DataAccess;
using CoffeeShop.Domain.UserManagement.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PointOfSale.BusinessLogic.ApplicationUserManagement.Services;

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

        return services;
    }

    public static IServiceCollection AddUserManagement(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserMappingService, UserMappingService>();
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
        return services;
    }
}
