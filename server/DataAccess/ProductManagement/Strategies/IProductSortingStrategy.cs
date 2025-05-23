using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.DataAccess.ProductManagement.Strategies;

public interface IProductSortingStrategy
{
    IQueryable<Product> Sort(IQueryable<Product> products);
}