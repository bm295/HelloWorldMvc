using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<Order>> GetAllAsync()
        {
            return _context.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.Timestamp)
                .ToListAsync();
        }

        public Task<Order> GetByIdAsync(int orderId)
        {
            return _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
