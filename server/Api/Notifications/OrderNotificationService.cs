using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using Microsoft.AspNetCore.SignalR;
using CoffeeShop.BusinessLogic.Common.Exceptions;
using Polly;

namespace CoffeeShop.Api.Notifications;

public class OrderNotificationService : IOrderNotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;

    public OrderNotificationService(IHubContext<NotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendOrderReadyNotification(OrderDTO order)
    {
        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

        try
        {
            await retryPolicy.ExecuteAsync(async () =>
            {
                await _hubContext.Clients
                    .Group(order.Customer.Id)
                    .SendAsync("orderReady", new
                    {
                        orderId = order.Id,
                        pickupTime = order.PickupTime,
                    });
            });
        }
        catch
        {
            throw new NotificationDispatchException($"Failed to send notification to {order.Customer.Id}");
        }
    }
}