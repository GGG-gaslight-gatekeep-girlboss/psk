using CoffeeShop.BusinessLogic.Common.Entities;
using CoffeeShop.BusinessLogic.PaymentManagement.Enums;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Entities;

public class CardPayment : BaseEntity
{
    public required Guid OrderId { get; set; }
    public required decimal Amount { get; set; }
    public required PaymentStatus Status { get; set; }
    public required string IntentId { get; set; }
}