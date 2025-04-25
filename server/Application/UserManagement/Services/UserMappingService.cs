using CoffeeShop.Application.UserManagement.DTOs;
using CoffeeShop.Application.UserManagement.Interfaces;
using CoffeeShop.Domain.UserManagement.Entities;

namespace CoffeeShop.Application.UserManagement.Services;

public class UserMappingService : IUserMappingService
{
    public UserDTO MapUserDTO(User user, string role)
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
}
