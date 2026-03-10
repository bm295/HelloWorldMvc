using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Ports;

public interface IOrderRepositoryPort
{
    Task<List<Order>> GetAllAsync();
    Task<Order?> GetByIdAsync(int orderId);
    Task<Order> AddAsync(Order order);
    Task SaveChangesAsync();
}
