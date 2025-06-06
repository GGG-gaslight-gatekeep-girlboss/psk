using CoffeeShop.Api.DTOs;

namespace CoffeeShop.Api.Middlewares;

public class DevelopmentExceptionHandlingMiddleware : ExceptionHandlingMiddleware
{
    public DevelopmentExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<DevelopmentExceptionHandlingMiddleware> logger)
        : base(next, logger)
    {
    }

    protected override object CreateErrorForException(Exception ex)
    {
        return new DevelopmentErrorDTO
        {
            ErrorMessage = GetErrorMessageForException(ex),
            StackTrace = ex.StackTrace
        };
    }

    protected override string GetUnexpectedErrorMessage(Exception ex) => ex.Message;
}