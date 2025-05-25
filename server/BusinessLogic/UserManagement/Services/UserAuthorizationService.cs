using CoffeeShop.BusinessLogic.UserManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Exceptions;
using CoffeeShop.BusinessLogic.UserManagement.Interfaces;

namespace CoffeeShop.BusinessLogic.UserManagement.Services;

public class UserAuthorizationService : IUserAuthorizationService
{
    private readonly ICurrentUserAccessor _currentUserAccessor;

    public UserAuthorizationService(ICurrentUserAccessor currentUserAccessor)
    {
        _currentUserAccessor = currentUserAccessor;
    }

    public void AuthorizeUserActionOnEmployee(string userId)
    {
        if (
            _currentUserAccessor.GetCurrentUserRole() != nameof(Roles.BusinessOwner)
            && _currentUserAccessor.GetCurrentUserId() != userId
        )
        {
            throw new UserNotAuthorizedException();
        }
    }
}
