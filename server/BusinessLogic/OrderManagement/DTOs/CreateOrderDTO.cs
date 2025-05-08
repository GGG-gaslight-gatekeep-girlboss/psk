namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record CreateOrderDTO(
    DateTimeOffset PickupTime,
    List<CreateOrderItemDTO> Items
);