using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeShop.BusinessLogic.OrderManagement.Entities;

public class Order : BaseEntity
{
    public required DateTimeOffset PickupTime { get; set; }
    public required Status OrderStatus { get; set; }
    public required List<OrderItem> Items { get; set; }
    public bool IsDeleted { get; set; } = false;

    [NotMapped]
    public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
}