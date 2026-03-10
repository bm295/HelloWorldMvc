using MilkCoPOS.Application.Ports;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public class TableUseCaseService(ITableRepositoryPort tableRepository) : ITableUseCaseService
{
    public Task<List<DiningTable>> GetTablesAsync() => tableRepository.GetAllAsync();

    public Task<DiningTable?> GetTableAsync(int id) => tableRepository.GetByIdAsync(id);

    public Task<DiningTable> CreateTableAsync(DiningTable table) => tableRepository.AddAsync(table);
}
