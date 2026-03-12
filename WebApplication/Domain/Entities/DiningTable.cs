using MilkCoPOS.Domain.Enums;

namespace MilkCoPOS.Domain.Entities;

public class DiningTable
{
    public int TableId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int SeatCount { get; set; }
    public TableStatus Status { get; set; } = TableStatus.Available;
}
