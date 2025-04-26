using CoffeeShop.Api.UserManagement.DTOs;
using CoffeeShop.Api.UserManagement.Interfaces;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers;

[ApiController]
[Route("v1/users")]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserMappingService _userMappingService;

    public UserController(IUserService userService, IUserMappingService userMappingService)
    {
        _userService = userService;
        _userMappingService = userMappingService;
    }

    [HttpPost]
    [Route("register/client")]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterUserDTO request)
    {
        var user = _userMappingService.MapRegisterUserDTOToUser(request);
        await _userService.AddUser(user, request.Password, Roles.Client.ToString());

        return Ok(_userMappingService.MapUserToDTO(user, Roles.Client.ToString()));
    }

    [HttpPost]
    [Route("register/employee")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserDTO request)
    {
        var user = _userMappingService.MapRegisterUserDTOToUser(request);
        await _userService.AddUser(user, request.Password, Roles.Employee.ToString());

        return Ok(_userMappingService.MapUserToDTO(user, Roles.Employee.ToString()));
    }
}
