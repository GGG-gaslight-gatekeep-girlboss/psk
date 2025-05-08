using Microsoft.AspNetCore.Mvc;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;

namespace CoffeeShop.Api.OrderManagement.Controllers;

[ApiController]
[Route("v1/orders")]
public class OrderController : ControllerBase 
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    //TODO: Make endpoints
    [HttpPost]
    public async Task<ActionResult<OrderDTO>> CreateOrder([FromBody] CreateOrderDTO request)
    {
        return Ok(await _orderService.CreateOrder(request));
    }
}