namespace MilkCoPOS.ViewModels;

public class OrderConfirmationViewModel
{
    public int OrderId { get; set; }

    public string Customer { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public List<OrderConfirmationItemViewModel> Items { get; set; } = [];
}

public class OrderConfirmationItemViewModel
{
    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }
}
