using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
using CoffeeShop.BusinessLogic.PaymentManagement.Enums;
using CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;
using Stripe;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Services;

public class StripeService : IStripeService
{
    private readonly PaymentIntentService _paymentIntentService;

    public StripeService(PaymentIntentService paymentIntentService)
    {
        _paymentIntentService = paymentIntentService;
    }

    public async Task<PaymentIntentDTO> CreatePaymentIntent(decimal paymentAmount)
    {
        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = (long)(paymentAmount * 100m),
            Currency = "eur",
            PaymentMethodTypes = ["card"],
        };

        try
        {
            var paymentIntent = await _paymentIntentService.CreateAsync(paymentIntentOptions);

            return new PaymentIntentDTO
            {
                PaymentIntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
                PaymentId = Guid.Empty
            };
        }
        catch (StripeException ex)
        {
            throw new InvalidDomainValueException(ex.StripeError.Message);
        }
    }

    public async Task<PaymentStatus> GetPaymentIntentStatus(string paymentId)
    {
        var paymentIntent = await _paymentIntentService.GetAsync(paymentId);
        return paymentIntent.Status switch
        {
            "succeeded" => PaymentStatus.Succeeded,
            "canceled" => PaymentStatus.Canceled,
            _ => PaymentStatus.Pending,
        };
    }
}