using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.UserManagement.Controllers;

[ApiController]
[Route("v1/users")]
public sealed class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IUserAuthorizationService _userAuthorizationService;
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public UserController(
        IUserService userService,
        IUserAuthorizationService userAuthorizationService,
        ICurrentUserAccessor currentUserAccessor)
    {
        _userService = userService;
        _userAuthorizationService = userAuthorizationService;
        _currentUserAccessor = currentUserAccessor;
    }

    [HttpPost]
    [Route("clients")]
    public async Task<IActionResult> RegisterClient([FromBody] RegisterUserDTO request)
    {
        var client = await _userService.CreateUser(request, Roles.Client.ToString());

        return Ok(client);
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)}")]
    [Route("employees")]
    public async Task<ActionResult<List<UserDTO>>> GetAllEmployees()
    {
        return Ok(await _userService.GetAllUsersByRole(nameof(Roles.Employee)));
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    [Route("employees/{id}")]
    public async Task<IActionResult> GetEmployee(string id)
    {
        _userAuthorizationService.AuthorizeUserActionOnEmployee(id);

        var employee = await _userService.GetUserByIdAndRole(id, nameof(Roles.Employee));
        return Ok(employee);
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var user = await _userService.GetUserByIdAndRole(
            _currentUserAccessor.GetCurrentUserId(),
            _currentUserAccessor.GetCurrentUserRole());

        return Ok(user);
    }

    [HttpPost]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)}")]
    [Route("employees")]
    public async Task<IActionResult> RegisterEmployee([FromBody] RegisterUserDTO request)
    {
        var employee = await _userService.CreateUser(request, Roles.Employee.ToString());

        return Ok(employee);
    }

    [HttpPatch]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    [Route("employees/{id}")]
    public async Task<IActionResult> UpdateEmployee(string id, [FromBody] UpdateUserDTO request)
    {
        _userAuthorizationService.AuthorizeUserActionOnEmployee(id);

        var employee = await _userService.UpdateUser(id, request);
        return Ok(employee);
    }

    [HttpDelete]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)}")]
    [Route("employees/{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        await _userService.DeleteUser(id);
        return NoContent();
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<LoginResponseDTO>> LoginUser([FromBody] LoginUserDTO request)
    {
        var loginResponse = await _userService.AuthenticateUser(request);
        Response.Cookies.Append(
            UserManagement.Constants.AccessTokenName,
            loginResponse.AccessToken.Token,
            GetAccessTokenCookieOptions(loginResponse.AccessToken)
        );
        return Ok(loginResponse);
    }

    [HttpPost]
    [Authorize(
        Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)},{nameof(Roles.Client)}"
    )]
    [Route("logout")]
    public IActionResult LogoutUser()
    {
        Response.Cookies.Delete(UserManagement.Constants.AccessTokenName);
        return Ok();
    }

    private static CookieOptions GetAccessTokenCookieOptions(TokenDTO accessToken)
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Expires = accessToken.ExpiresAt
        };
    }
}
