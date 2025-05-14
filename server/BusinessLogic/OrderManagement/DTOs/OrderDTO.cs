namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record OrderDTO(
    Guid Id,
    OrderCustomerDTO Customer,
    string Status,
    List<OrderItemDTO> Items,
    decimal TotalPrice,
    DateTimeOffset PickupTime,
    DateTimeOffset CreatedAt
);