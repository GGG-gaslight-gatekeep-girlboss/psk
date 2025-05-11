using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;

namespace CoffeeShop.BusinessLogic.OrderManagement.Services;

public class OrderMappingService : IOrderMappingService{
    public Order MapCreateOrderDTOToOrder(
        CreateOrderDTO dto, 
        List<OrderItem> mappedItems, 
        Status orderStatus)
    {
        return new Order
        {
            PickupTime = dto.PickupTime,
            OrderStatus = orderStatus,
            Items = mappedItems
        };
    }

    public OrderDTO MapOrderToOrderDTO(Order order)
    {
        List<OrderItemDTO> mappedItems = MapOrderItemToOrderItemDTO(order.Items);
        return new OrderDTO(
            order.Id,
            order.CreatedById!,
            order.OrderStatus.ToString(),
            mappedItems,
            order.TotalPrice,
            order.PickupTime
        );
    }

    private List<OrderItemDTO> MapOrderItemToOrderItemDTO(List<OrderItem> items)
    {
        var mappedItems = new List<OrderItemDTO>();
        foreach (var item in items)
        {
            if (!item.ProductId.HasValue)
            {
                mappedItems.Add(new OrderItemDTO(
                    Guid.Empty,
                    "Deleted Product",
                    item.ProductPrice,
                    item.Quantity
                ));
                continue;
            }

            mappedItems.Add(new OrderItemDTO(
                item.ProductId.Value,
                item.ProductName,
                item.ProductPrice,
                item.Quantity
            ));
        }

        return mappedItems;
    }
}