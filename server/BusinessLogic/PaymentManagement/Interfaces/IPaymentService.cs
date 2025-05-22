using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
using CoffeeShop.BusinessLogic.PaymentManagement.Entities;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

public interface IPaymentService
{
    Task<PaymentIntentDTO> CreateCardPayment(Guid orderId, decimal paymentAmount);
    Task<CardPayment> ConfirmCardPayment(string paymentIntentId);
}