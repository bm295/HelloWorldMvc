using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.ViewModels;

public class OrderPageViewModel
{
    [Required]
    [StringLength(100)]
    public string Customer { get; set; } = string.Empty;

    public List<OrderLineViewModel> Items { get; set; } = [];
}

public class OrderLineViewModel
{
    public int InventoryItemId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int AvailableQuantity { get; set; }

    [Range(0, int.MaxValue)]
    public int RequestedQuantity { get; set; }
}
