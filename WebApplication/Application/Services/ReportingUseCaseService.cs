using MilkCoPOS.Application.Models;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public class ReportingUseCaseService(
    IOrderRepositoryPort orderRepository,
    IPaymentRepositoryPort paymentRepository,
    IInventoryRepositoryPort inventoryRepository,
    ITableRepositoryPort tableRepository) : IReportingUseCaseService
{
    public async Task<OperationsReport> GetOperationsSummaryAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        var payments = await paymentRepository.GetAllAsync();
        var inventory = await inventoryRepository.GetAllAsync();
        var tables = await tableRepository.GetAllAsync();

        var today = DateTime.UtcNow.Date;

        return new OperationsReport
        {
            OpenOrders = orders.Count(o => o.Status is OrderStatus.Draft or OrderStatus.SentToKitchen or OrderStatus.Paid),
            ClosedOrdersToday = orders.Count(o => o.Status == OrderStatus.Closed && o.ClosedAtUtc.HasValue && o.ClosedAtUtc.Value.Date == today),
            RevenueToday = payments.Where(p => p.ProcessedAtUtc.Date == today).Sum(p => p.Amount),
            LowStockItems = inventory.Count(i => i.Quantity <= 5),
            OccupiedTables = tables.Count(t => t.Status == TableStatus.Occupied),
            AvailableSeats = tables.Where(t => t.Status == TableStatus.Available).Sum(t => t.SeatCount)
        };
    }
}
