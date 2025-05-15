namespace CoffeeShop.BusinessLogic.Common.Exceptions;

public class EntityHasBeenModifiedException(string errorMessage) : CoffeeShopExceptionBase(errorMessage);