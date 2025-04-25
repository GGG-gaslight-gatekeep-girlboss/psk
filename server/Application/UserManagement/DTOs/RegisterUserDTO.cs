namespace CoffeeShop.Application.UserManagement.DTOs;

public sealed record RegisterUserDTO(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    string Role
);
