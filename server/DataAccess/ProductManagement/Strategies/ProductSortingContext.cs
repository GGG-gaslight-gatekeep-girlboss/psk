using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.DataAccess.ProductManagement.Strategies;

public class ProductSortingContext
{
    private readonly IProductSortingStrategy _productSortingStrategy;

    public ProductSortingContext(IProductSortingStrategy productSortingStrategy)
    {
        _productSortingStrategy = productSortingStrategy;
    }

    public IQueryable<Product> Sort(IQueryable<Product> products)
    {
        return _productSortingStrategy.Sort(products);
    }
}