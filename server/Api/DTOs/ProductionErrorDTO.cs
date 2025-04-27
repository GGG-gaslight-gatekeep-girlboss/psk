namespace CoffeeShop.Api.DTOs;

public record ProductionErrorDTO
{
    public required string ErrorMessage { get; init; }
}