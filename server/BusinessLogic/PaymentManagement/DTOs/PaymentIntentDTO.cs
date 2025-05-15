namespace CoffeeShop.BusinessLogic.PaymentManagement.DTOs;

public record PaymentIntentDTO
{
    public required string PaymentIntentId { get; init; }
    public required string ClientSecret { get; init; }
}