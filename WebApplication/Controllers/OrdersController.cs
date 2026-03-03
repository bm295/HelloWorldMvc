using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Models;
using MilkCoPOS.Repositories;
using MilkCoPOS.Services;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderRepository orderRepository, IOrderService orderService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Order>>> GetOrders()
    {
        var orders = await orderRepository.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Order>> GetOrder(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
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
}
