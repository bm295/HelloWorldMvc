using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Application.Services;
using MilkCoPOS.Domain.Entities;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController(IInventoryUseCaseService inventoryService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InventoryItem>>> GetInventory() =>
        Ok(await inventoryService.GetInventoryAsync());

    [HttpPost]
    public async Task<ActionResult<InventoryItem>> CreateItem([FromBody] InventoryItem item)
    {
        var created = await inventoryService.CreateItemAsync(item);
        return CreatedAtAction(nameof(GetItem), new { id = created.ItemId }, created);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InventoryItem>> GetItem(int id)
    {
        var item = await inventoryService.GetInventoryItemAsync(id);
        return item is null ? NotFound() : Ok(item);
    }
}
