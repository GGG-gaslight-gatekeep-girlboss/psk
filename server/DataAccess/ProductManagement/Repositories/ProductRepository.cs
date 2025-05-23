using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.DataAccess.Common.Repositories;
using CoffeeShop.DataAccess.ProductManagement.Strategies;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DataAccess.ProductManagement.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    private readonly ProductSortingContext _productSortingContext;
    public ProductRepository(ApplicationDbContext dbContext, ProductSortingContext productSortingContext)
        : base(dbContext)
    {
        _productSortingContext = productSortingContext;
    }

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
        var query = DbSet.Include(x => x.Image).AsQueryable();
        return await _productSortingContext.Sort(query).ToListAsync();
    }
    
    public override string GetEntityNotFoundErrorMessage(Guid id)
    {
        return $"Product with id {id} is not found";
    }
}