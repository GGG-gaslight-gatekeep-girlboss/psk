using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;
using Microsoft.AspNetCore.Http;

namespace CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

public interface IProductMappingService
{
    ProductDTO MapProductToProductDTO(Product product);
    Product MapCreateProductDTOToProduct(CreateProductDTO dto);
    SetProductImageDTO MapSetProductImageDTO(Guid productId, IFormFile file);
}