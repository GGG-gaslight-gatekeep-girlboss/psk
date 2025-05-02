using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface IUserAuthorizationService
{
    public void AuthorizeUserActionOnEmployee(string userId);
}
