using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;

namespace CoffeeShop.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderMappingService{
    Order MapCreateOrderDTOToOrder(CreateOrderDTO dto, List<OrderItem> mappedItems, Status orderStatus);
    OrderDTO MapOrderToOrderDTO(Order order);
}