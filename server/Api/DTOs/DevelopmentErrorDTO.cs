namespace CoffeeShop.Api.DTOs;

public record DevelopmentErrorDTO : ProductionErrorDTO
{
    public required string? StackTrace { get; init; }
}