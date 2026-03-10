using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Application.Services;
using MilkCoPOS.Models;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderUseCaseService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders() => Ok(await orderService.GetOrdersAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await orderService.GetOrderAsync(id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var result = await orderService.CreateOrderAsync(request);
        if (!result.Success || result.Order is null)
        {
            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(nameof(GetOrder), new { id = result.Order.OrderId }, result.Order);
    }

    [HttpPost("{orderId:int}/items")]
    public async Task<IActionResult> AddItem(int orderId, [FromBody] AddOrderItemRequest request)
    {
        var result = await orderService.AddItemAsync(orderId, request);
        return result.Success ? Ok() : BadRequest(new { message = result.Error });
    }

    [HttpDelete("{orderId:int}/items/{orderItemId:int}")]
    public async Task<IActionResult> RemoveItem(int orderId, int orderItemId)
    {
        var result = await orderService.RemoveItemAsync(orderId, orderItemId);
        return result.Success ? Ok() : BadRequest(new { message = result.Error });
    }

    [HttpPost("{orderId:int}/send-to-kitchen")]
    public async Task<IActionResult> SendToKitchen(int orderId)
    {
        var result = await orderService.SendToKitchenAsync(orderId);
        return result.Success ? Ok() : BadRequest(new { message = result.Error });
    }

    [HttpPost("{orderId:int}/close")]
    public async Task<IActionResult> CloseOrder(int orderId)
    {
        var result = await orderService.CloseOrderAsync(orderId);
        return result.Success ? Ok() : BadRequest(new { message = result.Error });
    }
}
