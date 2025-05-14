using CoffeeShop.DataAccess.Common.Repositories;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.OrderManagement.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository{
    public OrderRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<List<Order>> GetAllByUserId(string userId)
    {
        return await DbSet
            .Where(order => order.CreatedById == userId && !order.IsDeleted)
            .Include(order => order.Items)
            .ToListAsync();
    }

    public async Task<List<Order>> GetAllWithItems()
    {
        return await DbSet
            .Where(order => !order.IsDeleted)
            .Include(order => order.Items)
            .Include(order => order.CreatedBy)
            .ToListAsync();
    }

    public async Task<Order> GetWithItems(Guid id)
    {
        var entity = await DbSet
            .Where(x => x.Id == id && !x.IsDeleted)
            .Include(x => x.Items)
            .Include(order => order.CreatedBy)
            .FirstOrDefaultAsync();

        return entity ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(id));
    }

    public override string GetEntityNotFoundErrorMessage(Guid id)
    {
        return $"Order with id {id} is not found";
    }
}