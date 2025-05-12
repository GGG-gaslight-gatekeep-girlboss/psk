namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record CreateOrderItemDTO(
    Guid ProductId,
    int Quantity
);