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

    public async Task<PaymentIntentDTO> CreatePaymentIntent(CreatePaymentIntentDTO paymentIntentDTO)
    {
        var paymentIntentOptions = new PaymentIntentCreateOptions
        {
            Amount = (long)(paymentIntentDTO.PaymentAmount * 100m),
            Currency = "eur",
            PaymentMethodTypes = ["card"],
            Description = $"Payment for order #{paymentIntentDTO.OrderId} ({paymentIntentDTO.PaymentAmount}€ tip)",
        };

        try
        {
            var paymentIntent = await _paymentIntentService.CreateAsync(paymentIntentOptions);

            return new PaymentIntentDTO
            {
                PaymentIntentId = paymentIntent.Id,
                ClientSecret = paymentIntent.ClientSecret,
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

    public Task CancelPaymentIntent(string paymentId) =>
        _paymentIntentService.CancelAsync(paymentId);
}