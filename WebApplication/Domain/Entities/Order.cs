using MilkCoPOS.Domain.Enums;

namespace MilkCoPOS.Domain.Entities;

public class Order
{
    public int OrderId { get; set; }
    public string Customer { get; set; } = string.Empty;
    public int TableId { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public OrderStatus Status { get; set; } = OrderStatus.Draft;
    public DateTime? SentToKitchenAtUtc { get; set; }
    public DateTime? ClosedAtUtc { get; set; }
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
