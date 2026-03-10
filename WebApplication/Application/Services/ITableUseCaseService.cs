using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public interface ITableUseCaseService
{
    Task<List<DiningTable>> GetTablesAsync();
    Task<DiningTable?> GetTableAsync(int id);
    Task<DiningTable> CreateTableAsync(DiningTable table);
}
