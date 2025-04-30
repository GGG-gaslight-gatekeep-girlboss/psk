using CoffeeShop.BusinessLogic.ProductManagement.DTOs;

namespace CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

public interface IProductService
{
    Task<ProductDTO> CreateProduct(CreateProductDTO dto);
    Task<ProductDTO> GetProduct(Guid id);
    Task DeleteProduct(Guid id);
    Task<ProductDTO> UpdateProduct(Guid id, UpdateProductDTO dto);
    Task<List<ProductDTO>> GetAllProducts();
}