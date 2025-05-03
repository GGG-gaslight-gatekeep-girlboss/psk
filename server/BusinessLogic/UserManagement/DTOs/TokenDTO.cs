namespace CoffeeShop.BusinessLogic.UserManagement.DTOs;

public sealed record TokenDTO(string Token, DateTimeOffset ExpiresAt);
