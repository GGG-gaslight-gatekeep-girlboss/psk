namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface ICurrentUserAccessor
{
    string GetCurrentUserId();
    string GetCurrentUserRole();
}
