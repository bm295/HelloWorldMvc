using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Infrastructure.Persistence;

public class OrderRepositoryAdapter(ApplicationDbContext context) : IOrderRepositoryPort
{
    public Task<List<Order>> GetAllAsync() => context.Orders
        .Include(o => o.Items)
        .OrderByDescending(o => o.Timestamp)
        .ToListAsync();

    public Task<Order?> GetByIdAsync(int orderId) => context.Orders
        .Include(o => o.Items)
        .FirstOrDefaultAsync(o => o.OrderId == orderId);

    public async Task<Order> AddAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
