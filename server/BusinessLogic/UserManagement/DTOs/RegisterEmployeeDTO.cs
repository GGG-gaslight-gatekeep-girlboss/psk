namespace CoffeeShop.BusinessLogic.UserManagement.DTOs;

public sealed record RegisterEmployeeDTO(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    Guid BusinessId
);
