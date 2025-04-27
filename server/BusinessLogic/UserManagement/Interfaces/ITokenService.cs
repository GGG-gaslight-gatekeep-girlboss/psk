using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, string role);
}
