using CoffeeShop.BusinessLogic.PaymentManagement.DTOs;
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
    [Route("intent")]
    public async Task<ActionResult<PaymentIntentDTO>> CreateCardPaymentIntent(
        [FromBody] PaymentOrderIdentifierDTO paymentOrderIdentifier)
    {
        var paymentIntent = await _paymentService.CreateCardPaymentIntent(paymentOrderIdentifier.OrderId);
        return Ok(paymentIntent);
    }
    
    [HttpPost]
    [Route("intent/{paymentIntentId}/confirm")]
    public async Task<IActionResult> ConfirmCardPayment(
        [FromRoute] string paymentIntentId)
    {
        await _paymentService.ConfirmCardPayment(paymentIntentId);
        return NoContent();
    }
}