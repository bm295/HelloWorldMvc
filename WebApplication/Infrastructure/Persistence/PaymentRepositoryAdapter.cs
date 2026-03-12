using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Data;
using MilkCoPOS.Domain.Entities;

namespace MilkCoPOS.Infrastructure.Persistence;

public class PaymentRepositoryAdapter(ApplicationDbContext context) : IPaymentRepositoryPort
{
    public Task<List<Payment>> GetAllAsync() => context.Payments
        .OrderByDescending(p => p.ProcessedAtUtc)
        .ToListAsync();

    public Task<Payment?> GetByIdAsync(int id) => context.Payments
        .FirstOrDefaultAsync(p => p.PaymentId == id);

    public async Task<Payment> AddAsync(Payment payment)
    {
        context.Payments.Add(payment);
        await context.SaveChangesAsync();
        return payment;
    }
}
