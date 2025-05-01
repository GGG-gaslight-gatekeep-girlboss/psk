using CoffeeShop.BusinessLogic.ProductManagement.DTOs;

namespace CoffeeShop.BusinessLogic.ProductManagement.Interfaces;

public interface IProductImageService
{
    Task SetProductImage(SetProductImageDTO setProductImageDTO);
}