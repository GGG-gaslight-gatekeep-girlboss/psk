using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CoffeeShop.BusinessLogic.UserManagement.Services;

public class UserService : IUserService
{
    // TODO add user authorization service to validate actions based on user id and role
    private readonly UserManager<User> _userManager;
    private readonly IUserMappingService _userMappingService;
    private readonly ITokenService _tokenService;

    public UserService(
        UserManager<User> userManager,
        IUserMappingService userMappingService,
        ITokenService tokenService
    )
    {
        _userManager = userManager;
        _userMappingService = userMappingService;
        _tokenService = tokenService;
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

        var role = (await _userManager.GetRolesAsync(user)).First();

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

    public async Task<(UserDTO, TokenDTO)> AuthenticateUser(LoginUserDTO dto)
    {
        if (
            await _userManager.FindByEmailAsync(dto.Email) is { } user
            && await _userManager.CheckPasswordAsync(user, dto.Password)
            && user.IsActive
        )
        {
            var role = (await _userManager.GetRolesAsync(user)).First();
            var accessToken = _tokenService.GenerateAccessToken(user, role!);

            return (
                _userMappingService.MapUserToDTO(user, role!),
                _userMappingService.MapTokenDTO(accessToken)
            );
        }
        else
        {
            throw new UserNotAuthenticatedException("Invalid user credentials.");
        }
    }

    public async Task<List<UserDTO>> GetAllUsersByRole(string role)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(role);
        return usersInRole.Select(user => _userMappingService.MapUserToDTO(user, role)).ToList();
    }
}
