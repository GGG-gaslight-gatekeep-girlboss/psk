namespace CoffeeShop.BusinessLogic.PaymentManagement.DTOs;

public record PaymentOrderIdentifierDTO
{
    public required Guid OrderId { get; init; }
}