namespace CoffeeShop.BusinessLogic.Common.DTOs;

public record UploadImageDTO
{
    public required string Key { get; init; }
    public required Stream ImageStream { get; init; }
    public required string ContentType { get; init; }
}