using CoffeeShop.BusinessLogic.ProductManagement.DTOs;
using CoffeeShop.BusinessLogic.ProductManagement.Entities;

namespace CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

public interface IProductMappingService
{
    ProductDTO MapProductToProductDTO(Product product);
    Product MapCreateProductDTOToProduct(CreateProductDTO dto);
}