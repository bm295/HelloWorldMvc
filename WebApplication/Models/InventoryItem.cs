using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models;

public class InventoryItem
{
    [Key]
    public int ItemId { get; set; }

    [Required]
    [StringLength(120)]
    public string Name { get; set; } = string.Empty;

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Range(0, double.MaxValue)]
    public decimal UnitPrice { get; set; }

    [StringLength(16)]
    public string Unit { get; set; } = "portion";
}
