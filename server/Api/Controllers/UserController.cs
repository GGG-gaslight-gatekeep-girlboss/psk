using CoffeeShop.Application.UserManagement.DTOs;
using CoffeeShop.Application.UserManagement.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.Controllers;

[ApiController]
[Route("v1/users")]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO request)
    {
        var user = await _userService.CreateUser(request);

        return Ok(user);
    }
}
