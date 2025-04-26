namespace CoffeeShop.BusinessLogic.BusinessManagement.DTOs;

public record CreateBusinessDTO
{
    public required string BusinessOwnerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Email { get; set; }
}
