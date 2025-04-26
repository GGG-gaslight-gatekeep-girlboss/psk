namespace CoffeeShop.BusinessLogic.UserManagement.DTOs;

public sealed record UpdateUserDTO(
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber,
    string? Password
);
