using CoffeeShop.Api.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.Api.UserManagement.Interfaces;

public interface IUserMappingService
{
    public UserDTO MapUserToDTO(User user, string role);
    public User MapRegisterUserDTOToUser(RegisterUserDTO dto);
}
