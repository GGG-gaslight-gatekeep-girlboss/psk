using CoffeeShop.BusinessLogic.UserManagement.DTOs;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface IUserService
{
    Task<UserDTO> CreateUser(RegisterUserDTO dto, string role);
    Task<UserDTO> UpdateUser(string id, UpdateUserDTO user);
    Task DeleteUser(string id);
    Task<string> AuthenticateUser(LoginUserDTO dto);
}
