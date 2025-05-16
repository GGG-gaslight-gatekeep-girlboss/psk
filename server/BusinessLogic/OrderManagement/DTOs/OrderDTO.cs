using CoffeeShop.BusinessLogic.OrderManagement.Enums;

namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record OrderDTO(
    Guid Id,
    OrderCustomerDTO Customer,
    OrderStatus Status,
    List<OrderItemDTO> Items,
    decimal TotalPrice,
    DateTimeOffset PickupTime,
    DateTimeOffset CreatedAt
);