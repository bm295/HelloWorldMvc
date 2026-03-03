using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Payment>>> GetPayments() =>
        Ok(await context.Payments.ToListAsync());

    [HttpPost]
    public async Task<ActionResult<Payment>> CreatePayment([FromBody] Payment payment)
    {
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Payment>> GetPayment(int id)
    {
        var payment = await context.Payments.FindAsync(id);
        return payment is null ? NotFound() : Ok(payment);
    }
}
