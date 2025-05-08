using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderMappingService{
    Order MapCreateOrderDTOToOrder(CreateOrderDTO dto, string orderStatus);
    OrderDTO MapOrderToOrderDTO(Order order, List<OrderItemDTO> mappedItems);
}