using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Application.Services;
using MilkCoPOS.Models;
using MilkCoPOS.ViewModels;

namespace MilkCoPOS.Controllers;

public class OrderPageController(
    IInventoryUseCaseService inventoryService,
    IOrderUseCaseService orderService,
    ITableUseCaseService tableService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = await BuildPageModelAsync(null, string.Empty, null);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(OrderPageViewModel postedModel)
    {
        var requestedQuantities = postedModel.Items.ToDictionary(
            item => item.InventoryItemId,
            item => item.RequestedQuantity);

        var model = await BuildPageModelAsync(requestedQuantities, postedModel.Customer, postedModel.TableId);

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

        if (model.Tables.All(t => t.TableId != model.TableId))
        {
            ModelState.AddModelError(string.Empty, "Please select a valid table.");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var request = new CreateOrderRequest
        {
            Customer = model.Customer,
            TableId = model.TableId,
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
            var refreshedModel = await BuildPageModelAsync(requestedQuantities, postedModel.Customer, postedModel.TableId);
            return View(refreshedModel);
        }

        return RedirectToAction(nameof(Confirmation), new { id = result.Order.OrderId });
    }

    [HttpGet]
    public async Task<IActionResult> Confirmation(int id)
    {
        var order = await orderService.GetOrderAsync(id);
        if (order is null)
        {
            return NotFound();
        }

        var inventoryItems = await inventoryService.GetInventoryAsync();
        var inventoryNames = inventoryItems.ToDictionary(item => item.ItemId, item => item.Name);

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
        string customer,
        int? selectedTableId)
    {
        var inventoryItems = await inventoryService.GetInventoryAsync();
        var tables = await tableService.GetTablesAsync();

        var defaultTableId = selectedTableId ?? tables.FirstOrDefault()?.TableId ?? 0;

        return new OrderPageViewModel
        {
            Customer = customer,
            TableId = defaultTableId,
            Tables = tables
                .Where(t => t.Status == TableStatus.Available || t.TableId == defaultTableId)
                .Select(t => new TableOptionViewModel { TableId = t.TableId, Name = $"{t.Name} ({t.SeatCount} seats)" })
                .ToList(),
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
