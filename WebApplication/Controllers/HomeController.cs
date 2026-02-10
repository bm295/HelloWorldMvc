using Microsoft.AspNetCore.Mvc;

namespace MilkCoPOS.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { status = "ok" });
    }
}
