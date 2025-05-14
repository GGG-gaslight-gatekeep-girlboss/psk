using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CoffeeShop.BusinessLogic.OrderManagement.Interfaces;
using CoffeeShop.BusinessLogic.OrderManagement.DTOs;
using CoffeeShop.BusinessLogic.UserManagement.Enums;

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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> CreateOrder([FromBody] CreateOrderDTO request)
    {
        return Ok(await _orderService.CreateOrder(request));
    }

    [HttpGet]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet]
    [Authorize]
    [Route("me")]
    public async Task<ActionResult<List<OrderDTO>>> GetCurrentUserOrders()
    {
        var orders = await _orderService.GetCurrentUserOrders();
        return Ok(orders);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize]
    public async Task<ActionResult<OrderDTO>> GetOrder(Guid id)
    {
        return Ok(await _orderService.GetOrder(id));
    }

    [HttpPatch]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    [Route("{id:Guid}")]
    public async Task<ActionResult<OrderDTO>> UpdateOrderStatus(Guid id, [FromBody] UpdateOrderDTO request)
    {
        var order = await _orderService.UpdateOrderStatus(id, request);
        return Ok(order);
    }

    [HttpDelete]
    [Authorize(Roles = $"{nameof(Roles.BusinessOwner)},{nameof(Roles.Employee)}")]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        await _orderService.DeleteOrder(id);
        return NoContent();
    } 
}