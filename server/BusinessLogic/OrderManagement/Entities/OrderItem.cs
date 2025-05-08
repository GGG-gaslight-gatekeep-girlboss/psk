using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Entities;

public class OrderItem : BaseEntity
{
    public Guid? OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Guid? ProductId { get; set; }
    public Product Product { get; set; } = null!;
    
    public required int Quantity { get; set; }
}