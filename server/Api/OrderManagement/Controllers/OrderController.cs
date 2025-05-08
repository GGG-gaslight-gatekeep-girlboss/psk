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

    [HttpGet]
    public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
    {
        return Ok(await _orderService.GetOrder(id));
    }
}