namespace CoffeeShop.BusinessLogic.Common.Exceptions;

public abstract class CoffeeShopExceptionBase : Exception
{
    public string ErrorMessage { get; init; }

    protected CoffeeShopExceptionBase(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}