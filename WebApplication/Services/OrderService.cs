using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;
using MilkCoPOS.Repositories;

namespace MilkCoPOS.Services;

public class OrderService(ApplicationDbContext context, IOrderRepository orderRepository) : IOrderService
{
    public async Task<(bool Success, string? Error, Order? Order)> CreateOrderAsync(CreateOrderRequest request)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        var inventoryIds = request.Items.Select(i => i.InventoryItemId).Distinct().ToList();
        var inventoryItems = await context.Inventory
            .Where(i => inventoryIds.Contains(i.ItemId))
            .ToListAsync();

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

        var order = new Order
        {
            Customer = request.Customer,
            Timestamp = DateTime.UtcNow,
            Items = request.Items.Select(i => new OrderItem
            {
                InventoryItemId = i.InventoryItemId,
                Quantity = i.Quantity
            }).ToList()
        };

        var createdOrder = await orderRepository.AddAsync(order);
        await transaction.CommitAsync();
        return (true, null, createdOrder);
    }
}
