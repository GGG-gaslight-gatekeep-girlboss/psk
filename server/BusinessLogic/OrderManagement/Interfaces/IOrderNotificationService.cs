using CoffeeShop.BusinessLogic.OrderManagement.DTOs;

namespace CoffeeShop.BusinessLogic.OrderManagement.Interfaces;

public interface IOrderNotificationService {
    Task SendOrderReadyNotification(OrderDTO order);
}