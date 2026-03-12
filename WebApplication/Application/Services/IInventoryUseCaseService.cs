using MilkCoPOS.Domain.Entities;

namespace MilkCoPOS.Application.Services;

public interface IInventoryUseCaseService
{
    Task<List<InventoryItem>> GetInventoryAsync();
    Task<InventoryItem?> GetInventoryItemAsync(int id);
    Task<InventoryItem> CreateItemAsync(InventoryItem item);
}
