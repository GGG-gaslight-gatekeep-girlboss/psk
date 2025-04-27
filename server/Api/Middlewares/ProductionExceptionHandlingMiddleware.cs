using CoffeeShop.Api.DTOs;

namespace CoffeeShop.Api.Middlewares;

public class ProductionExceptionHandlingMiddleware : ExceptionHandlingMiddleware
{
    public ProductionExceptionHandlingMiddleware(RequestDelegate next) : base(next)
    {
    }
    
    protected override object CreateErrorForException(Exception ex)
    {
        return new ProductionErrorDTO()
        {
            ErrorMessage = GetErrorMessageForException(ex)
        };
    }

    protected override string GetUnexpectedErrorMessage(Exception _) => "An unexpected error occurred.";
}