using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
using CoffeeShop.BusinessLogic.PaymentManagement.Enums;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

public interface IStripeService
{
    Task<PaymentIntentDTO> CreatePaymentIntent(CreatePaymentIntentDTO paymentIntentDTO);
    Task<PaymentStatus> GetPaymentIntentStatus(string paymentId);
    Task CancelPaymentIntent(string paymentId);
}