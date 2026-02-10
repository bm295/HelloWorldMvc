using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Payment>>> GetPayments()
        {
            return Ok(await _context.Payments.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<Payment>> CreatePayment(Payment payment)
        {
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPayment), new { id = payment.PaymentId }, payment);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
    }
}
