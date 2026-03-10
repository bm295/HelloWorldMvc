using MilkCoPOS.Application.Ports;
using MilkCoPOS.Models;

namespace MilkCoPOS.Application.Services;

public class OrderUseCaseService(
    IOrderRepositoryPort orderRepository,
    IInventoryRepositoryPort inventoryRepository,
    ITableRepositoryPort tableRepository) : IOrderUseCaseService
{
    public Task<List<Order>> GetOrdersAsync() => orderRepository.GetAllAsync();

    public Task<Order?> GetOrderAsync(int orderId) => orderRepository.GetByIdAsync(orderId);

    public async Task<(bool Success, string? Error, Order? Order)> CreateOrderAsync(CreateOrderRequest request)
    {
        var table = await tableRepository.GetByIdAsync(request.TableId);
        if (table is null)
        {
            return (false, "Table not found.", null);
        }

        if (table.Status == TableStatus.OutOfService)
        {
            return (false, "Table is out of service.", null);
        }

        var inventoryIds = request.Items.Select(i => i.InventoryItemId).Distinct().ToList();
        var inventoryItems = await inventoryRepository.GetByIdsAsync(inventoryIds);

        if (inventoryItems.Count != inventoryIds.Count)
        {
            return (false, "One or more inventory items do not exist.", null);
        }

        foreach (var itemRequest in request.Items)
        {
            var inventory = inventoryItems.First(i => i.ItemId == itemRequest.InventoryItemId);
            if (inventory.Quantity < itemRequest.Quantity)
            {
                return (false, $"Insufficient stock for item {inventory.Name}.", null);
            }

            inventory.Quantity -= itemRequest.Quantity;
        }

        table.Status = TableStatus.Occupied;

        var order = new Order
        {
            Customer = request.Customer,
            TableId = request.TableId,
            Timestamp = DateTime.UtcNow,
            Status = OrderStatus.Draft,
            Items = request.Items.Select(i => new OrderItem
            {
                InventoryItemId = i.InventoryItemId,
                Quantity = i.Quantity
            }).ToList()
        };

        var createdOrder = await orderRepository.AddAsync(order);
        await inventoryRepository.SaveChangesAsync();
        await tableRepository.SaveChangesAsync();

        return (true, null, createdOrder);
    }

    public async Task<(bool Success, string? Error)> AddItemAsync(int orderId, AddOrderItemRequest request)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return (false, "Order not found.");
        }

        if (order.Status is OrderStatus.Paid or OrderStatus.Closed)
        {
            return (false, "Cannot modify a paid or closed order.");
        }

        var inventory = await inventoryRepository.GetByIdAsync(request.InventoryItemId);
        if (inventory is null || inventory.Quantity < request.Quantity)
        {
            return (false, "Inventory unavailable for requested quantity.");
        }

        inventory.Quantity -= request.Quantity;
        order.Items.Add(new OrderItem { InventoryItemId = request.InventoryItemId, Quantity = request.Quantity });

        await orderRepository.SaveChangesAsync();
        await inventoryRepository.SaveChangesAsync();

        return (true, null);
    }

    public async Task<(bool Success, string? Error)> RemoveItemAsync(int orderId, int orderItemId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return (false, "Order not found.");
        }

        if (order.Status is OrderStatus.Paid or OrderStatus.Closed)
        {
            return (false, "Cannot modify a paid or closed order.");
        }

        var item = order.Items.FirstOrDefault(i => i.OrderItemId == orderItemId);
        if (item is null)
        {
            return (false, "Order item not found.");
        }

        var inventory = await inventoryRepository.GetByIdAsync(item.InventoryItemId);
        if (inventory is not null)
        {
            inventory.Quantity += item.Quantity;
        }

        order.Items.Remove(item);
        await orderRepository.SaveChangesAsync();
        await inventoryRepository.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> SendToKitchenAsync(int orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return (false, "Order not found.");
        }

        if (order.Status != OrderStatus.Draft)
        {
            return (false, "Only draft orders can be sent to kitchen.");
        }

        order.Status = OrderStatus.SentToKitchen;
        order.SentToKitchenAtUtc = DateTime.UtcNow;
        await orderRepository.SaveChangesAsync();
        return (true, null);
    }

    public async Task<(bool Success, string? Error)> CloseOrderAsync(int orderId)
    {
        var order = await orderRepository.GetByIdAsync(orderId);
        if (order is null)
        {
            return (false, "Order not found.");
        }

        if (order.Status != OrderStatus.Paid)
        {
            return (false, "Only paid orders can be closed.");
        }

        order.Status = OrderStatus.Closed;
        order.ClosedAtUtc = DateTime.UtcNow;

        var table = await tableRepository.GetByIdAsync(order.TableId);
        if (table is not null)
        {
            table.Status = TableStatus.Available;
            await tableRepository.SaveChangesAsync();
        }

        await orderRepository.SaveChangesAsync();
        return (true, null);
    }
}
