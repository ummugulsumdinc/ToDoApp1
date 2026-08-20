using Microsoft.AspNetCore.Mvc; // paket dahil ettik
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces; // Arayüzü kullanmak için dahil ettik
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ToDoApp1.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        // Dependency Injection: Asıl işi yapacak servisi içeri alıyoruz.
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
        public IActionResult GetAll([FromQuery] bool? isCompleted, [FromQuery] string?  sortBy)
        {
            
            var list = _toDoService.GetAll(isCompleted, sortBy);

            return Ok(list);
        }

        [HttpGet("{id}")] 
        public IActionResult GetById(int id)
        {
            var item = _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı."); // 404 Not Found 
            }

            // Kayıt bulunduysa 200 OK 
            return Ok(item);
        }

        [HttpGet("search")]
        public IActionResult Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))// null ya da sadece space girildiyse
            {
                return BadRequest("Arama yapabilmek için bir kelime girmelisiniz.");
            }

            var result = _toDoService.Search(query);
            return Ok(result);
        }

        [HttpPost] // yeni veri oluşturmak eklemek için
        public IActionResult Post([FromBody] ToDoCreateDto newItem)
        {
            var validationResult = _createValidator.Validate(newItem);

           
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _toDoService.PostAdd(newItem);

            return Created(string.Empty, "Kayıt başarıyla eklendi.");
        }

        [HttpPut("{id}")] 
        public IActionResult Put(int id, [FromBody] ToDoUpdateDto updatedItem)
        {
            var item = _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı."); // 404 Not Found 
            }
            var validationResult = _updateValidator.Validate(updatedItem);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _toDoService.Update(id,updatedItem);

            return Ok("Kayıt başarıyla güncellendi.");
        }

        [HttpDelete("{id}")] // var olan veriyi siler
        public IActionResult Delete(int id)
        {
            var item = _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı."); // 404 Not Found 
            }
            _toDoService.Delete(id);

            return Ok("Kayıt başarıyla silindi.");
        }

        [HttpPatch("{id}/complete")]
        public IActionResult CompleteToDo(int id)
        {
            var existingItem=_toDoService.GetById(id);// bu id ile bir kayıt var mı 
            if(existingItem == null)
            {
                return NotFound("İstenen kayıt bulunamadı");
            }
            _toDoService.MarkAsComplete(id);
            return Ok("Kayıt başarıyla tamamlandı olarak işaretlendi");
        }
    }
}