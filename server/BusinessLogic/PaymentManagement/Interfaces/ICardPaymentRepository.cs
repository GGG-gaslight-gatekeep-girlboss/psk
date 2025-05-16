using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.PaymentManagement.Entities;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

public interface ICardPaymentRepository : IBaseRepository<CardPayment>
{
    Task<CardPayment> GetPaymentByIntentId(string intentId);
}