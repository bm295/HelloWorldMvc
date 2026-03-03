using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.Models;

public class CreateOrderRequest
{
    [Required]
    [StringLength(100)]
    public string Customer { get; set; } = string.Empty;

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
