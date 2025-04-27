namespace CoffeeShop.BusinessLogic.Common.Exceptions;

public class InvalidDomainValueException(string errorMessage) : CoffeeShopExceptionBase(errorMessage);
