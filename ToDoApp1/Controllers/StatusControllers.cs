using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Dtos.Status;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Controllers
{
    [Route("api/statuses")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusService _statusService;

        public StatusesController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var statuses = await _statusService.GetAll();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var status = await _statusService.GetById(id);
            if (status == null)
            {
                return NotFound("Durum bulunamadı.");
            }
            return Ok(status);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StatusCreateDto statusDto)
        {
            await _statusService.Add(statusDto);
            return Created(string.Empty, "Durum başarıyla eklendi.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StatusCreateDto statusDto)
        {
            var status = await _statusService.GetById(id);
            if (status == null)
            {
                return NotFound("Güncellenecek durum bulunamadı.");
            }

            await _statusService.Update(id, statusDto);
            return Ok("Durum başarıyla güncellendi.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _statusService.GetById(id);
            if (status == null)
            {
                return NotFound("Silinecek durum bulunamadı.");
            }

            await _statusService.Delete(id);
            return Ok("Durum başarıyla silindi.");
        }
    }
}