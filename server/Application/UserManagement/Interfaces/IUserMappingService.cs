using CoffeeShop.Application.UserManagement.DTOs;
using CoffeeShop.Domain.UserManagement.Entities;

namespace CoffeeShop.Application.UserManagement.Interfaces;

public interface IUserMappingService
{
    public UserDTO MapUserDTO(User user, string role);
}
