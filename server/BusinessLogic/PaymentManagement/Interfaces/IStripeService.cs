using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
using CoffeeShop.BusinessLogic.PaymentManagement.Enums;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

public interface IStripeService
{
    Task<PaymentIntentDTO> CreatePaymentIntent(decimal paymentAmount);
    Task<PaymentStatus> GetPaymentIntentStatus(string paymentId);
}