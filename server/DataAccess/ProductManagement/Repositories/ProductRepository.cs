using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.DataAccess.Common.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.ProductManagement.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public async Task<Product> GetWithImage(Guid productId)
    {
        var entity = await DbSet
            .Where(x => x.Id == productId)
            .Include(x => x.Image)
            .FirstOrDefaultAsync();

        return entity ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(productId));
    }

    public async Task<List<Product>> GetAllWithImages()
    {
        return await DbSet
            .Include(x => x.Image)
            .ToListAsync();
    }
    
    public override string GetEntityNotFoundErrorMessage(Guid id)
    {
        return $"Product with id {id} is not found";
    }
}