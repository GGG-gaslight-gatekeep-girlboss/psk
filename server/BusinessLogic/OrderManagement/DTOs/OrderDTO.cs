namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record OrderDTO(
    Guid Id,
    string OrderStatus,
    List<OrderItemDTO> Items,
    decimal TotalPrice,
    DateTimeOffset PickupTime
);