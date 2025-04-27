using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.BusinessLogic.UserManagement.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserMappingService _userMappingService;

    public UserService(UserManager<User> userManager, IUserMappingService userMappingService)
    {
        _userManager = userManager;
        _userMappingService = userMappingService;
    }

    public async Task<UserDTO> CreateUser(RegisterUserDTO dto, string role)
    {
        var user = _userMappingService.MapRegisterUserDTOToUser(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidDomainValueException(errors);
        }

        return _userMappingService.MapUserToDTO(user, role);
    }

    public async Task<UserDTO> UpdateUser(string id, UpdateUserDTO dto)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null || !user.IsActive)
        {
            throw new UserNotFoundException(id);
        }

        if (dto.FirstName is not null)
        {
            user.FirstName = dto.FirstName;
        }

        if (dto.LastName is not null)
        {
            user.LastName = dto.LastName;
        }

        if (dto.Email is not null)
        {
            user.Email = dto.Email;
        }

        if (dto.PhoneNumber is not null)
        {
            user.PhoneNumber = dto.PhoneNumber;
        }

        if (dto.Password is not null)
        {
            await _userManager.RemovePasswordAsync(user);
            await _userManager.AddPasswordAsync(user, dto.Password);
        }

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidDomainValueException(errors);
        }

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        return _userMappingService.MapUserToDTO(user, role!);
    }

    public async Task DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null || !user.IsActive)
        {
            throw new UserNotFoundException(id);
        }

        user.Email = $"ANONYMIZED{user.Id}@ANONYMIZED.COM";
        user.PhoneNumber = "ANONYMIZED";
        user.FirstName = "ANONYMIZED";
        user.LastName = "ANONYMIZED";
        user.UserName = "ANONYMIZED";
        user.IsActive = false;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(errors);
        }
    }
}
