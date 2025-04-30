using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

namespace CoffeeShop.BusinessLogic.ProductManagement.Services;

public class ProductMappingService : IProductMappingService
{
    public ProductDTO MapProductToProductDTO(Product product)
    {
        return new ProductDTO(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ImageUrl,
            product.Stock
        );
        
    }

    public Product MapCreateProductDTOToProduct(CreateProductDTO dto)
    {
        return new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = Math.Round(dto.Price, 2),
            ImageUrl = dto.ImageUrl,
            Stock = dto.Stock
        };
    }
}