using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
using CoffeeShop.BusinessLogic.PaymentManagement.Entities;
using CoffeeShop.BusinessLogic.PaymentManagement.Enums;
using CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Services;

public class PaymentService : IPaymentService
{
    private readonly IStripeService _stripeService;
    private readonly ICardPaymentRepository _cardPaymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(
        IStripeService stripeService,
        ICardPaymentRepository cardPaymentRepository,
        IUnitOfWork unitOfWork)
    {
        _stripeService = stripeService;
        _cardPaymentRepository = cardPaymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentIntentDTO> CreateCardPayment(Guid orderId, decimal paymentAmount)
    {
        var paymentIntent = await _stripeService.CreatePaymentIntent(paymentAmount);

        var payment = new CardPayment
        {
            OrderId = orderId,
            Amount = paymentAmount,
            Status = PaymentStatus.Pending,
            IntentId = paymentIntent.PaymentIntentId
        };
        
        _cardPaymentRepository.Add(payment);
        await _unitOfWork.SaveChanges();

        return paymentIntent with { PaymentId = payment.Id };
    }

    public async Task<CardPayment> ConfirmCardPayment(string paymentIntentId)
    {
        var payment = await _cardPaymentRepository.GetPaymentByIntentId(paymentIntentId);
        var stripePaymentStatus = await _stripeService.GetPaymentIntentStatus(paymentIntentId);
        if (stripePaymentStatus == PaymentStatus.Succeeded)
        {
            payment.Status = PaymentStatus.Succeeded;
            await _unitOfWork.SaveChanges();
        }

        return payment;
    }
}