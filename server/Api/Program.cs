using CoffeeShop.Api.Extensions;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder
    .Services.AddSharedServices(builder.Environment, builder.Configuration)
    .AddUserManagement()
    .AddBusinessManagement();

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

app.MapControllers();

app.Run();
