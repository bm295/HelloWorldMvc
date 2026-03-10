using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public interface IInventoryUseCaseService
{
    Task<List<InventoryItem>> GetInventoryAsync();
    Task<InventoryItem?> GetInventoryItemAsync(int id);
    Task<InventoryItem> CreateItemAsync(InventoryItem item);
}
