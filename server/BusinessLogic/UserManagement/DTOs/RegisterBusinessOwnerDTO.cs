namespace CoffeeShop.BusinessLogic.UserManagement.DTOs;

public sealed record RegisterBusinessOwnerDTO(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password
);
