using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Application.Services;
using MilkCoPOS.Models;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(IPaymentUseCaseService paymentService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Payment>>> GetPayments() => Ok(await paymentService.GetPaymentsAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        var payment = await paymentService.GetPaymentAsync(id);
        return payment is null ? NotFound() : Ok(payment);
    }

    [HttpPost("orders/{orderId:int}")]
    public async Task<ActionResult<Payment>> ProcessPayment(int orderId, [FromBody] ProcessPaymentRequest request)
    {
        var result = await paymentService.ProcessPaymentAsync(orderId, request);
        if (!result.Success || result.Payment is null)
        {
            return BadRequest(new { message = result.Error });
        }

        return CreatedAtAction(nameof(GetPayment), new { id = result.Payment.PaymentId }, result.Payment);
    }
}
