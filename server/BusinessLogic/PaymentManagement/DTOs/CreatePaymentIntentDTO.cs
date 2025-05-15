namespace CoffeeShop.BusinessLogic.PaymentManagement.DTOs;

public record CreatePaymentIntentDTO
{
    public required Guid OrderId { get; init; }
    public required decimal PaymentAmount { get; init; }
}