namespace CoffeeShop.BusinessLogic.ProductManagement.DTOs;

public sealed record ProductDTO(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    int Stock
);