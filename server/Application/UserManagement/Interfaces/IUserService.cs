using CoffeeShop.Application.UserManagement.DTOs;

namespace CoffeeShop.Application.UserManagement.Interfaces;

public interface IUserService
{
    Task<UserDTO> CreateUser(RegisterUserDTO dto);
}
