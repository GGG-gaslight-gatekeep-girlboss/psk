using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.DataAccess.ProductManagement.Repositories;

public class CachedProductRepository : IProductRepository
{
    private const string ProductsWithImagesCacheKey = "products_with_images";
    
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachedProductRepository> _logger;

    public CachedProductRepository(
        IProductRepository productRepository,
        IMemoryCache cache,
        ILogger<CachedProductRepository> logger)
    {
        _productRepository = productRepository;
        _cache = cache;
        _logger = logger;
    }

    public void Add(Product entity)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        _logger.LogInformation("Products cache invalidated");
        _productRepository.Add(entity);
    }

    public Task<Product> Get(Guid id) => _productRepository.Get(id);

    public Task<List<Product>> GetMany(IEnumerable<Guid> ids) => _productRepository.GetMany(ids);

    public Task Delete(Guid id)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        _logger.LogInformation("Products cache invalidated");
        return _productRepository.Delete(id);
    }

    public void DeleteMany(IEnumerable<Product> entities)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        _logger.LogInformation("Products cache invalidated");
        _productRepository.DeleteMany(entities);
    }

    public void Update(Product entity)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        _logger.LogInformation("Products cache invalidated");
        _productRepository.Update(entity);
    }

    public Task<List<Product>> GetAll() => _productRepository.GetAll();

    public Task<Product> GetWithImage(Guid productId) => _productRepository.GetWithImage(productId);

    public Task<List<Product>> GetAllWithImages()
    {
        return _cache.GetOrCreateAsync(
            ProductsWithImagesCacheKey,
            _ =>
            {
                _logger.LogInformation("Products cache miss");
                return _productRepository.GetAllWithImages();
            })!;
    }

    public string GetEntityNotFoundErrorMessage(Guid id) => _productRepository.GetEntityNotFoundErrorMessage(id);
}