using System.ComponentModel.DataAnnotations;
using CoffeeShop.Application.UserManagement.DTOs;
using CoffeeShop.Application.UserManagement.Interfaces;
using CoffeeShop.Domain.UserManagement.Entities;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.Application.UserManagement.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserMappingService _userMappingService;

    public UserService(UserManager<User> userManager, IUserMappingService userMappingService)
    {
        _userManager = userManager;
        _userMappingService = userMappingService;
    }

    public async Task<UserDTO> CreateUser(RegisterUserDTO dto)
    {
        var user = new User
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            PhoneNumber = dto.PhoneNumber,
        };

        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, dto.Role);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(errors);
        }

        return _userMappingService.MapUserDTO(user, dto.Role);
    }
}
