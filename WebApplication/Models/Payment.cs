using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    public int OrderId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Amount { get; set; }

    [StringLength(50)]
    public string Method { get; set; } = "Cash";

    [StringLength(30)]
    public string Status { get; set; } = "Captured";

    public DateTime ProcessedAtUtc { get; set; } = DateTime.UtcNow;
}
