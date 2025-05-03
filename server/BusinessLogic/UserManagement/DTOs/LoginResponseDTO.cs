namespace CoffeeShop.BusinessLogic.UserManagement.DTOs;

public record LoginResponseDTO(UserDTO User, TokenDTO AccessToken);