using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Services;
using ToDoApp1.Business.Interfaces;
namespace ToDoApp1.Controllers
{

    
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculator _calculateService;
        public CalculatorController(ICalculator calculateService)
        {
            _calculateService = calculateService;
        }
        [HttpPost("calc")]
        public IActionResult CalculatePost([FromBody] CalculateDto calculation)
        {
            try
            {
                
                double result = _calculateService.Hesapla(calculation);

                return Ok(new { Result = result });
            }
            catch (Exception ex)
            {
                
                return BadRequest(ex.Message);
            }
        }
    }
}
