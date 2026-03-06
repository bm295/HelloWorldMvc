using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;
using MilkCoPOS.Repositories;
using MilkCoPOS.Services;
using MilkCoPOS.ViewModels;

namespace MilkCoPOS.Controllers;

public class OrderPageController(
    ApplicationDbContext context,
    IOrderService orderService,
    IOrderRepository orderRepository) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await BuildPageModelAsync(null, string.Empty);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(OrderPageViewModel postedModel)
    {
        var requestedQuantities = postedModel.Items.ToDictionary(
            item => item.InventoryItemId,
            item => item.RequestedQuantity);

        var model = await BuildPageModelAsync(requestedQuantities, postedModel.Customer);

        var selectedItems = model.Items
            .Where(item => item.RequestedQuantity > 0)
            .ToList();

        if (!selectedItems.Any())
        {
            ModelState.AddModelError(string.Empty, "Select at least one inventory item to place an order.");
        }

        foreach (var item in selectedItems.Where(item => item.RequestedQuantity > item.AvailableQuantity))
        {
            ModelState.AddModelError(
                string.Empty,
                $"Requested quantity for {item.Name} exceeds the available stock.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var request = new CreateOrderRequest
        {
            Customer = model.Customer,
            Items = selectedItems.Select(item => new CreateOrderItemRequest
            {
                InventoryItemId = item.InventoryItemId,
                Quantity = item.RequestedQuantity
            }).ToList()
        };

        var result = await orderService.CreateOrderAsync(request);
        if (!result.Success || result.Order is null)
        {
            ModelState.AddModelError(string.Empty, result.Error ?? "Unable to place the order.");
            var refreshedModel = await BuildPageModelAsync(requestedQuantities, postedModel.Customer);
            return View(refreshedModel);
        }

        return RedirectToAction(nameof(Confirmation), new { id = result.Order.OrderId });
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        var inventoryItemIds = order.Items
            .Select(item => item.InventoryItemId)
            .Distinct()
            .ToList();

        var inventoryNames = await context.Inventory
            .Where(item => inventoryItemIds.Contains(item.ItemId))
            .ToDictionaryAsync(item => item.ItemId, item => item.Name);

        var model = new OrderConfirmationViewModel
        {
            OrderId = order.OrderId,
            Customer = order.Customer,
            Timestamp = order.Timestamp,
            Items = order.Items
                .OrderBy(item => inventoryNames.TryGetValue(item.InventoryItemId, out var name) ? name : item.InventoryItemId.ToString())
                .Select(item => new OrderConfirmationItemViewModel
                {
                    Name = inventoryNames.TryGetValue(item.InventoryItemId, out var name)
                        ? name
                        : $"Item #{item.InventoryItemId}",
                    Quantity = item.Quantity
                })
                .ToList()
        };

        return View(model);
    }

    private async Task<OrderPageViewModel> BuildPageModelAsync(
        IReadOnlyDictionary<int, int>? requestedQuantities,
        string customer)
    {
        var inventoryItems = await context.Inventory
            .OrderBy(item => item.Name)
            .ToListAsync();

        return new OrderPageViewModel
        {
            Customer = customer,
            Items = inventoryItems.Select(item => new OrderLineViewModel
            {
                InventoryItemId = item.ItemId,
                Name = item.Name,
                AvailableQuantity = item.Quantity,
                RequestedQuantity = requestedQuantities is not null &&
                                    requestedQuantities.TryGetValue(item.ItemId, out var requestedQuantity)
                    ? requestedQuantity
                    : 0
            }).ToList()
        };
    }
}
