using MilkCoPOS.Application.Ports;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public class InventoryUseCaseService(IInventoryRepositoryPort inventoryRepository) : IInventoryUseCaseService
{
    public Task<List<InventoryItem>> GetInventoryAsync() => inventoryRepository.GetAllAsync();

    public Task<InventoryItem?> GetInventoryItemAsync(int id) => inventoryRepository.GetByIdAsync(id);

    public Task<InventoryItem> CreateItemAsync(InventoryItem item) => inventoryRepository.AddAsync(item);
}
