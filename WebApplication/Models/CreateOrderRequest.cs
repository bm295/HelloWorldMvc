using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models;

public class CreateOrderRequest
{
    [Required]
    [StringLength(100)]
    public string Customer { get; set; } = string.Empty;

    [Required]
    public int TableId { get; set; }

    [Required]
    [MinLength(1)]
    public List<CreateOrderItemRequest> Items { get; set; } = [];
}

public class CreateOrderItemRequest
{
    [Required]
    public int InventoryItemId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

public class AddOrderItemRequest
{
    [Required]
    public int InventoryItemId { get; set; }

    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}

public class ProcessPaymentRequest
{
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(50)]
    public string Method { get; set; } = "Cash";
}
