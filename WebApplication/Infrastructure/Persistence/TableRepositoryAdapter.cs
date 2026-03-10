using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Infrastructure.Persistence;

public class TableRepositoryAdapter(ApplicationDbContext context) : ITableRepositoryPort
{
    public Task<List<DiningTable>> GetAllAsync() => context.Tables
        .OrderBy(t => t.Name)
        .ToListAsync();

    public Task<DiningTable?> GetByIdAsync(int tableId) => context.Tables
        .FirstOrDefaultAsync(t => t.TableId == tableId);

    public async Task<DiningTable> AddAsync(DiningTable table)
    {
        context.Tables.Add(table);
        await context.SaveChangesAsync();
        return table;
    }

    public Task SaveChangesAsync() => context.SaveChangesAsync();
}
