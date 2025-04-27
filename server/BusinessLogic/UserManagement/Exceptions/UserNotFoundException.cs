using CoffeeShop.BusinessLogic.Common.Exceptions;

namespace CoffeeShop.BusinessLogic.UserManagement.Exceptions;

public class UserNotFoundException(string userId) : EntityNotFoundException($"User not found. ID: {userId}");