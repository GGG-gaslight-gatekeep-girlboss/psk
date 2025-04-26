using CoffeeShop.BusinessLogic.BusinessManagement.Entities;
using CoffeeShop.BusinessLogic.BusinessManagement.Interfaces;
using CoffeeShop.DataAccess.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.BusinessManagement.Repositories;

public class BusinessRepository : BaseRepository<Business>, IBusinessRepository
{
    public BusinessRepository(ApplicationDbContext dbContext)
        : base(dbContext) { }

    public async Task<Business> GetWithEmployees(Guid id)
    {
        var entity = await DbSet.Include(b => b.Employees).FirstOrDefaultAsync(b => b.Id == id);
        if (entity is not null)
        {
            return entity;
        }

        throw new KeyNotFoundException($"Entity with ID {id} was not found.");
    }
}
