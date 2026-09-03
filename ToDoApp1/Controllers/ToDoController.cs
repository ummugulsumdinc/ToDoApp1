using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Dtos.ToDo;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Controllers
{
    [Authorize]
    [Route("api/todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;
        private readonly IValidator<ToDoCreateDto> _createValidator;
        private readonly IValidator<ToDoUpdateDto> _updateValidator;

        public ToDoController(IToDoService toDoService,
            IValidator<ToDoCreateDto> createValidator,
            IValidator<ToDoUpdateDto> updateValidator)
        {
            _toDoService = toDoService;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        [HttpGet]
        //  bool? isCompleted yerine int? statusId yazdık ve metodu async yaptık
        public async Task<IActionResult> GetAll([FromQuery] int? statusId, [FromQuery] string? sortBy, [FromQuery] int page = 1)
        {
            int pageSize = 3;

            var list = await _toDoService.GetAll(statusId, sortBy,page, pageSize);
            return Ok(new
            {
                Message = $"{page}. sayfa görevler başarıyla listelendi.",
                Data = list
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı.");
            }

            return Ok(new
            {
                Message = "Görev detayı başarıyla getirildi.",
                Data = item
            });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Arama yapabilmek için bir kelime girmelisiniz.");
            }

            var result = await _toDoService.Search(query);
            return Ok(new
            {
                Message = $"{result.Count} adet arama sonucu bulundu.",
                Data = result
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ToDoCreateDto newItem)
        {
            var validationResult = _createValidator.Validate(newItem);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var createdItem =await _toDoService.PostAdd(newItem); // await eklendi

            return Created("", new
            {
                Message="Yeni görev başarıyla oluşturuldu.",
                Data=createdItem
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ToDoUpdateDto updatedItem)
        {
            var item = await _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı.");
            }
            var validationResult = _updateValidator.Validate(updatedItem);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await _toDoService.Update(id, updatedItem);

            return Ok(new
            {
                Message = "Kayıt başarıyla güncellendi.",
                Data=item
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı.");
            }

            await _toDoService.Delete(id);

            return Ok(new
            {
                Message = "Kayıt başarıyla silindi.",
                Data= item
            });
        }

        [HttpPatch("{id}/complete")]
        public async Task<IActionResult> CompleteToDo(int id)
        {
            var existingItem = await _toDoService.GetById(id);
            if (existingItem == null)
            {
                return NotFound("İstenen kayıt bulunamadı");
            }

            await _toDoService.MarkAsComplete(id);
            return Ok(new
            {
                Message = "Kayıt başarıyla tamamlandı olarak işaretlendi",
                Data=existingItem
            });
        }
    }
}
