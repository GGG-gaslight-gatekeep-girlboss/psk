using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;

namespace CoffeeShop.Api.UserManagement.Services;

public class UserMappingService : IUserMappingService
{
    public UserDTO MapUserToDTO(User user, string role)
    {
        return new UserDTO(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email!,
            user.PhoneNumber!,
            role
        );
    }

    public User MapRegisterUserDTOToUser(RegisterUserDTO dto)
    {
        return new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
            EmployerBusinessId = dto.BusinessId ?? null,
        };
    }
}
