using CoffeeShop.BusinessLogic.Common.Exceptions;

namespace CoffeeShop.BusinessLogic.UserManagement.Exceptions;

public class UserNotAuthenticatedException(string errorMessage)
    : CoffeeShopExceptionBase(errorMessage);
