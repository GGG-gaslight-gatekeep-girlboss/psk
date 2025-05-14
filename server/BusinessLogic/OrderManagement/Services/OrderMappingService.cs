using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.OrderManagement.Entities;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using CoffeeShop.BusinessLogic.UserManagement.Entities;

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
            MapUserToOrderCustomerDTO(order.CreatedBy),
            order.OrderStatus.ToString(),
            mappedItems,
            order.TotalPrice,
            order.PickupTime,
            order.CreatedAt
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
                    item.Quantity,
                    item.TotalPrice
                ));
                continue;
            }

            mappedItems.Add(new OrderItemDTO(
                item.ProductId.Value,
                item.ProductName,
                item.ProductPrice,
                item.Quantity,
                item.TotalPrice
            ));
        }

        return mappedItems;
    }

    private OrderCustomerDTO MapUserToOrderCustomerDTO(User user)
    {
        return new OrderCustomerDTO(
            user.Id,
            user.FirstName,
            user.LastName,
            user.PhoneNumber!
        );
    }
}