using CoffeeShop.Api.DTOs;

namespace CoffeeShop.Api.Middlewares;

public class DevelopmentExceptionHandlingMiddleware : ExceptionHandlingMiddleware
{
    public DevelopmentExceptionHandlingMiddleware(RequestDelegate next) : base(next)
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