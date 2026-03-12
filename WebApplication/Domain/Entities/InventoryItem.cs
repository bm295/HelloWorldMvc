namespace MilkCoPOS.Domain.Entities;

public class InventoryItem
{
    public int ItemId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Unit { get; set; } = "portion";
}
