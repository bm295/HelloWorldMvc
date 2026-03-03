using MilkCoPOS.Models;

namespace MilkCoPOS.Repositories;

public interface IOrderRepository
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int orderId);
    Task<Order> AddAsync(Order order);
}
