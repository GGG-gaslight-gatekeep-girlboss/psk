using CoffeeShop.BusinessLogic.UserManagement.Interfaces;

namespace CoffeeShop.Api.Middlewares;

public class InvokedEndpointLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public InvokedEndpointLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext httpContext,
        ICurrentUserAccessor currentUserAccessor,
        ILogger<InvokedEndpointLoggingMiddleware> logger)
    {
        var isAuthenticated = currentUserAccessor.IsAuthenticated();
        var controllerName = httpContext.Request.RouteValues["controller"]?.ToString();
        var actionName = httpContext.Request.RouteValues["action"]?.ToString();
        var endpoint = $"{controllerName}.{actionName}";
        var userId = isAuthenticated ? currentUserAccessor.GetCurrentUserId() : "None";
        var userRole = isAuthenticated ? currentUserAccessor.GetCurrentUserRole() : "None";
        var timestamp = DateTimeOffset.UtcNow;
        
        if (isAuthenticated)
        {
            logger.LogInformation("Endpoint {Endpoint} called by user {UserId} ({Role}) at {Timestamp}", endpoint, userId, userRole, timestamp);
        }
        else
        {
            logger.LogInformation("Endpoint {Endpoint} called by unauthenticated user at {Timestamp}", endpoint, timestamp);
        }
        
        await _next(httpContext);
    }
}