using CoffeeShop.BusinessLogic.UserManagement.Entities;

namespace CoffeeShop.BusinessLogic.UserManagement.Interfaces;

public interface IUserService
{
    Task<User> AddUser(User user, string password, string role);
}
