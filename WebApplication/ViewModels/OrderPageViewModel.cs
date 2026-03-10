using System.ComponentModel.DataAnnotations;

namespace MilkCoPOS.ViewModels;

public class OrderPageViewModel
{
    [Required]
    [StringLength(100)]
    public string Customer { get; set; } = string.Empty;

    [Required]
    public int TableId { get; set; }

    public List<TableOptionViewModel> Tables { get; set; } = [];

    public List<OrderLineViewModel> Items { get; set; } = [];
}

public class TableOptionViewModel
{
    public int TableId { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class OrderLineViewModel
{
    public int InventoryItemId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int AvailableQuantity { get; set; }

    [Range(0, int.MaxValue)]
    public int RequestedQuantity { get; set; }
}
