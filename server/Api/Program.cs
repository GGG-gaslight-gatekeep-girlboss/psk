using CoffeeShop.Api.Extensions;
using CoffeeShop.Api.Middlewares;
using CoffeeShop.Api.Notifications;
using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Identity;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables().AddDotNetEnv(".env");

builder
    .Services.AddSharedServices(builder.Environment, builder.Configuration)
    .AddOrderManagement()
    .AddProductManagement(builder.Configuration)
    .AddUserManagement(builder.Configuration)
    .AddPaymentManagement(builder.Configuration)
    .AddNotifications(builder.Configuration);

builder.Host.UseSerilog(
    (context, loggerConfig) =>
        loggerConfig
            .ReadFrom.Configuration(context.Configuration)
            .Filter.ByExcluding(
                (e) =>
                    e.Properties.ContainsKey("RequestPath")
                    && e.Properties["RequestPath"] is ScalarValue sv
                    && sv.Value?.ToString() == "/health"
            )
);

var app = builder.Build();

app.MapOpenApi();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "Coffee Shop");
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.ApplyMigrations();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = Enum.GetNames(typeof(Roles));

    foreach (var role in roles)
    {
        var roleExist = await roleManager.RoleExistsAsync(role);
        if (!roleExist)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
    var initialBusinessOwnerDTO = new RegisterUserDTO(
        FirstName: builder.Configuration["BusinessOwnerDetails:FirstName"]!,
        LastName: builder.Configuration["BusinessOwnerDetails:LastName"]!,
        Email: builder.Configuration["BusinessOwnerDetails:Email"]!,
        PhoneNumber: builder.Configuration["BusinessOwnerDetails:PhoneNumber"]!,
        Password: builder.Configuration["BusinessOwnerDetails:InitialPassword"]!
    );

    await userService.CreateBusinessOwnerIfNeeded(initialBusinessOwnerDTO);
}

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseMiddleware<DevelopmentExceptionHandlingMiddleware>();
}
else
{
    app.UseMiddleware<ProductionExceptionHandlingMiddleware>();
}

app.UseAuthentication();
app.UseAuthorization();

if (app.Configuration.GetValue<bool>("Logging:LogInvokedEndpoint"))
{
    app.UseMiddleware<InvokedEndpointLoggingMiddleware>();
}

app.MapControllers();

app.MapHealthChecks("/health");

app.MapHub<NotificationHub>("/hub/notifications");

app.Run();
