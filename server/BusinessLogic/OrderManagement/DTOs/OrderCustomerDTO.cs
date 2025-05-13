namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public record OrderCustomerDTO(
    string Id,
    string FirstName,
    string LastName,
    string PhoneNumber);