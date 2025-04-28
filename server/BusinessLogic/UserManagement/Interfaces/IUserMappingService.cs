using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface IUserMappingService
{
    public UserDTO MapUserToDTO(User user, string role);
    public User MapRegisterUserDTOToUser(RegisterUserDTO dto);
    public TokenDTO MapTokenDTO(string accessToken);
}
