using CoffeeShop.BusinessLogic.ProductManagement.DTOs;

namespace CoffeeShop.BusinessLogic.OrderManagement.DTOs;

public sealed record OrderItemDTO(
    ProductDTO productDTO,
    int Quantity
);