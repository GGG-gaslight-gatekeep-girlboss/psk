using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<Product> GetWithImage(Guid productId);
    Task<List<Product>> GetAllWithImages();
}