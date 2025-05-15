using CoffeeShop.BusinessLogic.Common.Exceptions;
using CoffeeShop.BusinessLogic.Common.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.Enums;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
using CoffeeShop.BusinessLogic.PaymentManagement.Entities;
using CoffeeShop.BusinessLogic.PaymentManagement.Enums;
using CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;

namespace CoffeeShop.BusinessLogic.PaymentManagement.Services;

public class PaymentService : IPaymentService
{
    private readonly IOrderService _orderService;
    private readonly IStripeService _stripeService;
    private readonly ICardPaymentRepository _cardPaymentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(
        IOrderService orderService,
        IStripeService stripeService,
        ICardPaymentRepository cardPaymentRepository,
        IUnitOfWork unitOfWork)
    {
        _orderService = orderService;
        _stripeService = stripeService;
        _cardPaymentRepository = cardPaymentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentIntentDTO> CreateCardPaymentIntent(Guid orderId)
    {
        var order = await _orderService.GetOrder(orderId);
        if (order.Status != OrderStatus.Pending)
        {
            throw new InvalidDomainValueException("Only pending order can be paid for.");
        }

        var paymentIntent = await _stripeService.CreatePaymentIntent(new CreatePaymentIntentDTO
        {
            OrderId = orderId,
            PaymentAmount = order.TotalPrice
        });

        var payment = new CardPayment
        {
            OrderId = orderId,
            Amount = order.TotalPrice,
            Status = PaymentStatus.Pending,
            IntentId = paymentIntent.PaymentIntentId
        };
        
        _cardPaymentRepository.Add(payment);
        await _unitOfWork.SaveChanges();

        return paymentIntent;
    }

    public async Task ConfirmCardPayment(string paymentIntentId)
    {
        var payment = await _cardPaymentRepository.GetPaymentByIntentId(paymentIntentId);
        var stripePaymentStatus = await _stripeService.GetPaymentIntentStatus(paymentIntentId);
        if (stripePaymentStatus == PaymentStatus.Succeeded)
        {
            payment.Status = PaymentStatus.Succeeded;
            await _unitOfWork.SaveChanges();
        }
    }
}