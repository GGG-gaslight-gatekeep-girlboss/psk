using CoffeeShop.BusinessLogic.Common.Exceptions;

namespace CoffeeShop.BusinessLogic.UserManagement.Exceptions;

public class UserNotAuthorizedException()
    : CoffeeShopExceptionBase("User has no access to perform the action.");
