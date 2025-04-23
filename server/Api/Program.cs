using CoffeeShop.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSharedServices(builder.Environment);

var app = builder.Build();

app.MapOpenApi();

app.UseSwaggerUI(options => {
    options.SwaggerEndpoint("/openapi/v1.json", "Coffee Shop");
});

if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors();

app.MapControllers();

app.Run();
