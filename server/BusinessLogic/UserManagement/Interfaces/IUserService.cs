using CoffeeShop.BusinessLogic.UserManagement.DTOs;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface IUserService
{
    Task<UserDTO> CreateUser(RegisterUserDTO dto, string role);
    Task<UserDTO> UpdateUser(string id, UpdateUserDTO user);
    Task DeleteUser(string id);
    Task<LoginResponseDTO> AuthenticateUser(LoginUserDTO dto);
    Task<List<UserDTO>> GetAllUsersByRole(string role);
    Task<UserDTO> GetUserByIdAndRole(string id, string role);
    Task CreateBusinessOwnerIfNeeded(RegisterUserDTO dto);
}
