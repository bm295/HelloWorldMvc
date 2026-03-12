using MilkCoPOS.Domain.Entities;

namespace MilkCoPOS.Application.Ports;

public interface IInventoryRepositoryPort
{
    Task<List<InventoryItem>> GetAllAsync();
    Task<InventoryItem?> GetByIdAsync(int itemId);
    Task<List<InventoryItem>> GetByIdsAsync(IEnumerable<int> itemIds);
    Task<InventoryItem> AddAsync(InventoryItem item);
    Task SaveChangesAsync();
}
