namespace CoffeeShop.Application.UserManagement.DTOs;

public sealed record UserDTO(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Role
);
