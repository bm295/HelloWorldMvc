using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Infrastructure.Persistence;

public class InventoryRepositoryAdapter(ApplicationDbContext context) : IInventoryRepositoryPort
{
    public Task<List<InventoryItem>> GetAllAsync() => context.Inventory
        .OrderBy(i => i.Name)
        .ToListAsync();

    public Task<InventoryItem?> GetByIdAsync(int itemId) => context.Inventory
        .FirstOrDefaultAsync(i => i.ItemId == itemId);

    public Task<List<InventoryItem>> GetByIdsAsync(IEnumerable<int> itemIds)
    {
        var ids = itemIds.Distinct().ToList();
        return context.Inventory.Where(i => ids.Contains(i.ItemId)).ToListAsync();
    }

    public async Task<InventoryItem> AddAsync(InventoryItem item)
    {
        context.Inventory.Add(item);
        await context.SaveChangesAsync();
        return item;
    }

    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
