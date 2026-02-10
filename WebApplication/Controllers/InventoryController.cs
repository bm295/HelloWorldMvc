using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilkCoPOS.Data;
using MilkCoPOS.Models;

namespace MilkCoPOS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InventoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<InventoryItem>>> GetInventory()
        {
            return Ok(await _context.Inventory.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<InventoryItem>> CreateItem(InventoryItem item)
        {
            _context.Inventory.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetItem), new { id = item.ItemId }, item);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryItem>> GetItem(int id)
        {
            var item = await _context.Inventory.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
    }
}
