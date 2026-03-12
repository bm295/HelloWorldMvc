using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Application.Services;
using MilkCoPOS.Domain.Entities;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TablesController(ITableUseCaseService tableService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<DiningTable>>> GetTables() => Ok(await tableService.GetTablesAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DiningTable>> GetTable(int id)
    {
        var table = await tableService.GetTableAsync(id);
        return table is null ? NotFound() : Ok(table);
    }

    [HttpPost]
    public async Task<ActionResult<DiningTable>> CreateTable([FromBody] DiningTable table)
    {
        var created = await tableService.CreateTableAsync(table);
        return CreatedAtAction(nameof(GetTable), new { id = created.TableId }, created);
    }
}
