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
    public async Task<IActionResult> ConfirmCardPayment([FromRoute] string paymentIntentId)
    {
        await _paymentService.ConfirmCardPayment(paymentIntentId);
        return NoContent();
    }
}