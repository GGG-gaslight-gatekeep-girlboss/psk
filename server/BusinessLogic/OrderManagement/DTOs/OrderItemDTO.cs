namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record OrderItemDTO(
    Guid ProductId,
    string ProductName,
    decimal ProductPrice,
    int Quantity,
    decimal TotalPrice
);