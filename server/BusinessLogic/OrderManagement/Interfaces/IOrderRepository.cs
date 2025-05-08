using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
public interface IOrderRepository : IBaseRepository<Order>{
    Task<List<Order>> GetAllByUserId(string userId);
    Task<List<Order>> GetAllWithItems();
    Task<Order> GetWithItems(Guid id);
}