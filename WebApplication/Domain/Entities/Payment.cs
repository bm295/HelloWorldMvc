namespace MilkCoPOS.Domain.Entities;

public class Payment
{
    public int PaymentId { get; set; }
    public int OrderId { get; set; }
    public decimal Amount { get; set; }
    public string Method { get; set; } = "Cash";
    public string Status { get; set; } = "Captured";
    public DateTime ProcessedAtUtc { get; set; } = DateTime.UtcNow;
}
