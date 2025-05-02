using System.Net;
using System.Text.Json;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;

namespace CoffeeShop.Api.Middlewares;

public abstract class ExceptionHandlingMiddleware
{
    private static readonly JsonSerializerOptions SerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    protected ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occurred: {Message}", ex.Message);
            
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)GetHttpStatusCodeForException(ex);

            var error = CreateErrorForException(ex);
            var serializedError = JsonSerializer.Serialize(error, SerializerOptions);
            await httpContext.Response.WriteAsync(serializedError);
        }
    }

    protected abstract object CreateErrorForException(Exception ex);

    protected abstract string GetUnexpectedErrorMessage(Exception ex);

    protected string GetErrorMessageForException(Exception ex) => ex switch
    {
        CoffeeShopExceptionBase e => e.ErrorMessage,
        _ => GetUnexpectedErrorMessage(ex)
    };
    
    private static HttpStatusCode GetHttpStatusCodeForException(Exception ex) => ex switch
    {
        EntityNotFoundException => HttpStatusCode.NotFound,
        InvalidDomainValueException => HttpStatusCode.BadRequest,
        UserNotAuthenticatedException => HttpStatusCode.Unauthorized,
        UserNotAuthorizedException => HttpStatusCode.Forbidden,
        _ => HttpStatusCode.InternalServerError
    };
}