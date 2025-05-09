using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;

namespace CoffeeShop.BusinessLogic.OrderManagement.Services;

public class OrderMappingService : IOrderMappingService{
    public Order MapCreateOrderDTOToOrder(CreateOrderDTO dto, string orderStatus)
    {
        List<OrderItem> mappedItems = MapCreateOrderItemDTOToOrderItem(dto.Items);
        return new Order
        {
            PickupTime = dto.PickupTime,
            OrderStatus = orderStatus,
            Items = mappedItems
        };
    }

    private List<OrderItem> MapCreateOrderItemDTOToOrderItem(List<CreateOrderItemDTO> dtos)
    {
        List<OrderItem> mappedItems = new();
        foreach (var dto in dtos)
        {
            mappedItems.Add(new OrderItem
            {
                ProductId = dto.ProductId,
                Quantity = dto.Quantity
            });
        }
        return mappedItems;
    }

    public OrderDTO MapOrderToOrderDTO(Order order, List<OrderItemDTO> mappedItems)
    {
        return new OrderDTO(
            order.Id,
            order.CreatedById,
            order.OrderStatus,
            mappedItems,
            order.TotalPrice(),
            order.PickupTime
        );
    }
}