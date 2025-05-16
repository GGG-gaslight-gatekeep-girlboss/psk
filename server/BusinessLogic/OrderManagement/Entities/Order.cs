using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using CoffeeShop.BusinessLogic.PaymentManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Entities;

public class Order : BaseEntity
{
    public required DateTimeOffset PickupTime { get; set; }
    public required OrderStatus OrderStatus { get; set; }
    public required List<OrderItem> Items { get; set; }
    public bool IsDeleted { get; set; } = false;
    public CardPayment Payment { get; set; }
    public Guid PaymentId { get; set; }

    [NotMapped]
    public decimal TotalPrice => Items.Sum(i => i.TotalPrice);
}