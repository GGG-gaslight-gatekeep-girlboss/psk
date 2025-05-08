namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record OrderItemDTO(
    Guid? ProductId,
    string? ProductName,
    int Quantity
);