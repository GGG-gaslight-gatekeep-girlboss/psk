using System.ComponentModel.DataAnnotations;
using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
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
        //TODO move validation to separate service
        if (role == Roles.Employee.ToString() && dto.BusinessId is null)
        {
            throw new ValidationException(
                "Employee must be associated with business. Provide employee business ID."
            );
        }
        else if (role != Roles.Employee.ToString() && dto.BusinessId is not null)
        {
            throw new ValidationException(
                "Only employees can be associated with business upon registration."
            );
        }

        var user = _userMappingService.MapRegisterUserDTOToUser(dto);
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
        else
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new ValidationException(errors);
        }

        return _userMappingService.MapUserToDTO(user, role);
    }

    public async Task<UserDTO> UpdateUser(string id, UpdateUserDTO dto)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with id {id} is not found");
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
            throw new ValidationException(errors);
        }

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        return _userMappingService.MapUserToDTO(user, role!);
    }

    public async Task DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new KeyNotFoundException($"User with id {id} is not found");
        }
        else
        {
            var result = await _userManager.DeleteAsync(user);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }
        }
    }
}
