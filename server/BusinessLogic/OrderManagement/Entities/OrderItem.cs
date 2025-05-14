using System.ComponentModel.DataAnnotations.Schema;
using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Entities;

public class OrderItem : BaseEntity
{
    public Guid? OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public Guid? ProductId { get; set; }
    public Product? Product { get; set; }

    public required string ProductName { get; set; }
    public required decimal ProductPrice { get; set; }
    public required int Quantity { get; set; }
    
    [NotMapped]
    public decimal TotalPrice => Quantity * ProductPrice;
}