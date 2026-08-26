using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Dtos.User;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Business.Services;

namespace ToDoApp1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase // İsmi senin projene uygun olarak UserController yaptık
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return NotFound("Aradığınız ID'ye ait kullanıcı bulunamadı.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserCreateDto userDto)
        {
            var createdUser = await _userService.Add(userDto);
            return Created(string.Empty, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserCreateDto userDto)
        {
            var existingUser = await _userService.GetById(id);
            if (existingUser == null)
            {
                return NotFound("Güncellenecek kullanıcı bulunamadı.");
            }

            await _userService.Update(id, userDto);
            return Ok("Kullanıcı başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingUser = await _userService.GetById(id);
            if (existingUser == null)
            {
                return NotFound("Silinecek kullanıcı bulunamadı.");
            }

            await _userService.Delete(id);
            return Ok("Kullanıcı başarıyla silindi.");
        }
    }
}