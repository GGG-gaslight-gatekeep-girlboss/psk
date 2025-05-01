namespace CoffeeShop.BusinessLogic.ProductManagement.DTOs;

public record SetProductImageDTO
{
    public required Guid ProductId { get; init; }
    public required Stream ImageStream { get; init; }
    public required long ImageSize { get; init; }
    public required string ContentType { get; init; }
    public required string FileName { get; init; }
}