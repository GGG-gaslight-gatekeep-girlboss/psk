namespace CoffeeShop.BusinessLogic.UserManagement.DTOs;

public sealed record RegisterClientDTO(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password
);
