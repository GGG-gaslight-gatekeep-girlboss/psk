using CoffeeShop.BusinessLogic.Common.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Entities;

public class Order : BaseEntity
{
    public required DateTimeOffset PickupTime { get; set; }
    public required string OrderStatus { get; set; }
    public required List<OrderItem> Items { get; set; }
}