namespace MilkCoPOS.Domain.Entities;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int InventoryItemId { get; set; }
    public int Quantity { get; set; }
}
