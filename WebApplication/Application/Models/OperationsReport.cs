namespace MilkCoPOS.Application.Models;

public class OperationsReport
{
    public int OpenOrders { get; set; }
    public int ClosedOrdersToday { get; set; }
    public decimal RevenueToday { get; set; }
    public int LowStockItems { get; set; }
    public int OccupiedTables { get; set; }
    public int AvailableSeats { get; set; }
}
