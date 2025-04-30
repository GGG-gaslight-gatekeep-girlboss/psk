namespace CoffeeShop.BusinessLogic.ProductManagement.DTOs;

public sealed record CreateProductDTO(
    string Name,
    string Description,
    decimal Price,  
    string ImageUrl,
    int Stock
);