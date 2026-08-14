using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;
using ToDoApp1.Validators;

namespace ToDoApp1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordCheckerController : ControllerBase
    {
        private readonly IPasswordChecker _passwordChecker;

        public PasswordCheckerController(IPasswordChecker passwordChecker)
        {
            _passwordChecker = passwordChecker;
        }
        [HttpGet("password")]
        //public IActionResult GetPasswordRules() { }

        [HttpPost("check")]
        public IActionResult CheckPassword([FromBody] PasswordCheckerRequestDto request)
        {
            var response = _passwordChecker.CheckPassword(request);

            return Ok(response);
        }

    }
}
