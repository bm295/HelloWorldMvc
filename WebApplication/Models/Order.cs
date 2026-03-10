using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [StringLength(100)]
    public string Customer { get; set; } = string.Empty;

    [Required]
    public int TableId { get; set; }

    public DiningTable? Table { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Draft;

    public DateTime? SentToKitchenAtUtc { get; set; }

    public DateTime? ClosedAtUtc { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
