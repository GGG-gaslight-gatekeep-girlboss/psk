namespace CoffeeShop.BusinessLogic.Common.Exceptions;

public class EntityNotFoundException(string errorMessage) : CoffeeShopExceptionBase(errorMessage);
