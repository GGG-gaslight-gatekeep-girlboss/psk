using CoffeeShop.BusinessLogic.UserManagement.Interfaces;
using Serilog.Context;

namespace CoffeeShop.Api.Middlewares;

public class UserContextLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, ICurrentUserAccessor currentUserAccessor)
    {
        if (currentUserAccessor.IsAuthenticated())
        {
            LogContext.PushProperty("UserId", currentUserAccessor.GetCurrentUserId());
            LogContext.PushProperty("UserRole", currentUserAccessor.GetCurrentUserRole());
        }
        
        await _next(httpContext);
    }
}