using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.DataAccess.ProductManagement.Strategies;

public class ProductSortingByPriceStrategy : IProductSortingStrategy
{
    public IQueryable<Product> Sort(IQueryable<Product> products)
    {
        return products.OrderBy(p => p.Price);
    }
}