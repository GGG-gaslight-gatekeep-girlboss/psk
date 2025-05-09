using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;

namespace CoffeeShop.BusinessLogic.OrderManagement.Entities;

public class Order : BaseEntity
{
    public required DateTimeOffset PickupTime { get; set; }
    public required Status OrderStatus { get; set; }
    public required List<OrderItem> Items { get; set; }
    public bool IsDeleted { get; set; } = false;
    public decimal TotalPrice(){
        return Items.Sum(item => (item.Product?.Price ?? 0) * item.Quantity);
    }
}