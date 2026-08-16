using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Application.Models;
using MilkCoPOS.Application.Ports;
using MilkCoPOS.Data;
using MilkCoPOS.Domain.Enums;

namespace MilkCoPOS.Infrastructure.Persistence;

public class ReportingRepositoryAdapter(ApplicationDbContext context) : IReportingRepositoryPort
{
    public async Task<OperationsReport> GetOperationsSummaryAsync(DateTime utcNow)
    {
        var startOfDayUtc = utcNow.Date;
        var startOfNextDayUtc = startOfDayUtc.AddDays(1);

        var openOrders = await context.Orders.CountAsync(order =>
            order.Status == OrderStatus.Draft ||
            order.Status == OrderStatus.SentToKitchen ||
            order.Status == OrderStatus.Paid);

        var closedOrdersToday = await context.Orders.CountAsync(order =>
            order.Status == OrderStatus.Closed &&
            order.ClosedAtUtc >= startOfDayUtc &&
            order.ClosedAtUtc < startOfNextDayUtc);

        var revenueToday = await context.Payments
            .Where(payment =>
                payment.ProcessedAtUtc >= startOfDayUtc &&
                payment.ProcessedAtUtc < startOfNextDayUtc)
            .SumAsync(payment => (decimal?)payment.Amount) ?? 0;

        var lowStockItems = await context.Inventory.CountAsync(item => item.Quantity <= 5);
        var occupiedTables = await context.Tables.CountAsync(table => table.Status == TableStatus.Occupied);
        var availableSeats = await context.Tables
            .Where(table => table.Status == TableStatus.Available)
            .SumAsync(table => (int?)table.SeatCount) ?? 0;

        return new OperationsReport
        {
            OpenOrders = openOrders,
            ClosedOrdersToday = closedOrdersToday,
            RevenueToday = revenueToday,
            LowStockItems = lowStockItems,
            OccupiedTables = occupiedTables,
            AvailableSeats = availableSeats
        };
    }
}
