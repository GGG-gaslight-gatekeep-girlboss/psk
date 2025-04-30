using CoffeeShop.Api.Extensions;
using CoffeeShop.Api.Middlewares;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using DotNetEnv.Configuration;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddDotNetEnv(".env");

builder
    .Services.AddSharedServices(builder.Environment, builder.Configuration)
    .AddProductManagement()
    .AddUserManagement(builder.Configuration);

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

app.MapControllers();

app.Run();
