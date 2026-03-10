using Microsoft.AspNetCore.Mvc;
using MilkCoPOS.Application.Models;
using MilkCoPOS.Application.Services;

namespace MilkCoPOS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController(IReportingUseCaseService reportingService) : ControllerBase
{
    [HttpGet("operations-summary")]
    public async Task<ActionResult<OperationsReport>> GetOperationsSummary() =>
        Ok(await reportingService.GetOperationsSummaryAsync());
}
