using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<PaymentIntentDTO> CreateCardPaymentIntent(Guid orderId);
    Task ConfirmCardPayment(string paymentIntentId);
}