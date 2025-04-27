using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
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
    [Route("business-owners")]
    public async Task<IActionResult> RegisterBusinessOwner([FromBody] RegisterUserDTO request)
    {
        var employee = await _userService.CreateUser(request, Roles.BusinessOwner.ToString());

        return Ok(employee);
    }

    [HttpPost]
    [Route("clients")]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterUserDTO request)
    {
        var client = await _userService.CreateUser(request, Roles.Client.ToString());

        return Ok(client);
    }

    [HttpPost]
    [Route("employees")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserDTO request)
    {
        var employee = await _userService.CreateUser(request, Roles.Employee.ToString());

        return Ok(employee);
    }

    [HttpPatch]
    [Route("employees/{id:Guid}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateUserDTO request)
    {
        var employee = await _userService.UpdateUser(id, request);
        return Ok(employee);
    }

    [HttpDelete]
    [Route("employees/{id:Guid}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }
}
