using CoffeeShop.BusinessLogic.OrderManagement.DTOs;

namespace CoffeeShop.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderService{
    Task<OrderDTO> CreateOrder(CreateOrderDTO dto);
    Task<List<OrderDTO>> GetAllOrders();
    Task<OrderDTO> GetOrder(Guid id);
}