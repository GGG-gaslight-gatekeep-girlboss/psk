using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;
using Microsoft.AspNetCore.Http;
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
            GetProductImageUrl(product),
            product.Version
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

    public SetProductImageDTO MapSetProductImageDTO(Guid productId, IFormFile file)
    {
        return new SetProductImageDTO
        {
            ProductId = productId,
            FileName = file.FileName,
            ImageSize = file.Length,
            ContentType = file.ContentType,
            ImageStream = file.OpenReadStream()
        };
    }

    private string? GetProductImageUrl(Product product)
    {
        return product.Image is null ? null : $"{_blobStorageUrl}/{product.Image.BlobKey}";
    }
}