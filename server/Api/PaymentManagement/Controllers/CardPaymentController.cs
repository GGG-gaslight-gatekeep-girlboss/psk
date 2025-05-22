using CoffeeShop.BusinessLogic.PaymentManagement.Entities;
using CoffeeShop.BusinessLogic.PaymentManagement.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Api.PaymentManagement.Controllers;

[Authorize]
[ApiController]
[Route("v1/payments/card")]
public class CardPaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public CardPaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpPost]
    [Route("{paymentIntentId}/confirm")]
    public async Task<ActionResult<CardPayment>> ConfirmCardPayment([FromRoute] string paymentIntentId)
    {
        var payment = await _paymentService.ConfirmCardPayment(paymentIntentId);
        return Ok(payment);
    }
}