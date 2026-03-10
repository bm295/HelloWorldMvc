using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models;

public class DiningTable
{
    [Key]
    public int TableId { get; set; }

    [Required]
    [StringLength(30)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 40)]
    public int SeatCount { get; set; }

    public TableStatus Status { get; set; } = TableStatus.Available;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
}
