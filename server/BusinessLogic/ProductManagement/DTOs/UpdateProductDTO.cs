namespace CoffeeShop.BusinessLogic.ProductManagement.DTOs;

public sealed record UpdateProductDTO(
    string? Name,
    string? Description,
    decimal? Price,
    string? ImageUrl,
    int? Stock
);