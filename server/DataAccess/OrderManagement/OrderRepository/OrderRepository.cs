using CoffeeShop.DataAccess.Common.Repositories;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;

namespace CoffeeShop.DataAccess.OrderManagement.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    //TODO: Override get to fetch the order items too
    public override string GetEntityNotFoundErrorMessage(Guid id)
    {
        return $"Order with id {id} is not found";
    }
}