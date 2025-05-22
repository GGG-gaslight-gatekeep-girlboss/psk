using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace CoffeeShop.DataAccess.ProductManagement.Repositories;

public class CachedProductRepository : IProductRepository
{
    private const string ProductsWithImagesCacheKey = "products_with_images";
    
    private readonly IProductRepository _productRepository;
    private readonly IMemoryCache _cache;

    public CachedProductRepository(
        IProductRepository productRepository,
        IMemoryCache cache)
    {
        _productRepository = productRepository;
        _cache = cache;
    }

    public void Add(Product entity) => _productRepository.Add(entity);

    public Task<Product> Get(Guid id) => _productRepository.Get(id);

    public Task<List<Product>> GetMany(IEnumerable<Guid> ids) => _productRepository.GetMany(ids);

    public Task Delete(Guid id)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        return _productRepository.Delete(id);
    }

    public void DeleteMany(IEnumerable<Product> entities)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        _productRepository.DeleteMany(entities);
    }

    public void Update(Product entity)
    {
        _cache.Remove(ProductsWithImagesCacheKey);
        _productRepository.Update(entity);
    }

    public Task<List<Product>> GetAll() => _productRepository.GetAll();

    public async Task<Product> GetWithImage(Guid productId)
    {
        var productsWithImages = _cache.Get<List<Product>>(ProductsWithImagesCacheKey);
        if (productsWithImages is not null)
        {
            return productsWithImages.FirstOrDefault(x => x.Id == productId)
                   ?? throw new EntityNotFoundException(GetEntityNotFoundErrorMessage(productId));
        }
        
        return await _cache.GetOrCreateAsync(
            ProductsWithImagesCacheKey,
            _ => _productRepository.GetWithImage(productId));
    }

    public Task<List<Product>> GetAllWithImages()
    {
        return _cache.GetOrCreateAsync(
            ProductsWithImagesCacheKey,
            _ => _productRepository.GetAllWithImages());
    }

    public string GetEntityNotFoundErrorMessage(Guid id) => _productRepository.GetEntityNotFoundErrorMessage(id);
}