using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Ports;

public interface ITableRepositoryPort
{
    Task<List<DiningTable>> GetAllAsync();
    Task<DiningTable?> GetByIdAsync(int tableId);
    Task<DiningTable> AddAsync(DiningTable table);
    Task SaveChangesAsync();
}
