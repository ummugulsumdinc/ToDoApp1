using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLogs([FromQuery] int count = 10, [FromQuery] string? level = null)
        {
            var result = await _logService.GetLogs(count, level);
            return Ok(result);
        }
    }
}