using System.Security.Claims;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CoffeeShop.BusinessLogic.UserManagement.Services;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor
            .HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)
            ?.Value;

        if (string.IsNullOrEmpty(userIdClaim))
        {
            throw new UserNotAuthenticatedException("Invalid access token.");
        }

        return userIdClaim;
    }

    public string GetCurrentUserRole()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value
            ?? throw new UserNotAuthenticatedException("Invalid access token.");
    }
}
