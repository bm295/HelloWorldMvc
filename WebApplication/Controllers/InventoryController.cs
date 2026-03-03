using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InventoryController(ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<InventoryItem>>> GetInventory() =>
        Ok(await context.Inventory.ToListAsync());

    [HttpPost]
    public async Task<ActionResult<InventoryItem>> CreateItem([FromBody] InventoryItem item)
    {
        context.Inventory.Add(item);
        await context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetItem), new { id = item.ItemId }, item);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<InventoryItem>> GetItem(int id)
    {
        var item = await context.Inventory.FindAsync(id);
        return item is null ? NotFound() : Ok(item);
    }
}
