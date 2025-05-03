using CoffeeShop.BusinessLogic.UserManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface ITokenService
{
    TokenDTO GenerateAccessToken(User user, string role);
}
