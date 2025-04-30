using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CoffeeShop.BusinessLogic.ProductManagement.Services;

public class ProductMappingService : IProductMappingService
{
    private readonly string _blobStorageUrl;
    
    public ProductMappingService(IConfiguration configuration)
    {
        _blobStorageUrl = configuration["BlobStorage:Url"]!;
    }
    
    public ProductDTO MapProductToProductDTO(Product product)
    {
        return new ProductDTO(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            GetProductImageUrl(product)
        );
    }

    public Product MapCreateProductDTOToProduct(CreateProductDTO dto)
    {
        return new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = Math.Round(dto.Price, 2),
            Stock = dto.Stock
        };
    }

    private string? GetProductImageUrl(Product product)
    {
        return product.Image is null ? null : $"{_blobStorageUrl}/{product.Image.BlobKey}";
    }
}