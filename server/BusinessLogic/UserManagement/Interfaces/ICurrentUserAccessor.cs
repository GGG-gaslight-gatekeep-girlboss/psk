namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface ICurrentUserAccessor
{
    bool IsAuthenticated();
    string GetCurrentUserId();
    string GetCurrentUserRole();
}
