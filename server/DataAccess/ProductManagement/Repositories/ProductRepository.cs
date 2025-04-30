using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using CoffeeShop.DataAccess.Common.Repositories;

namespace CoffeeShop.DataAccess.ProductManagement.Repositories;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(ApplicationDbContext dbContext) : base(dbContext) { }

    public override string GetEntityNotFoundErrorMessage(Guid id){
        return $"Product with id {id} is not found";
    }
}