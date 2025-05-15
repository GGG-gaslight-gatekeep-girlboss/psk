using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<PaymentIntentDTO> CreateCardPayment(Guid orderId, decimal paymentAmount);
    Task ConfirmCardPayment(string paymentIntentId);
}