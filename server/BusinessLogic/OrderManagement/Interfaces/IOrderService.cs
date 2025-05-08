using CoffeeShop.BusinessLogic.OrderManagement.DTOs;

namespace CoffeeShop.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderService{
    //TODO: Add methods
    Task<OrderDTO> CreateOrder(CreateOrderDTO dto);
}