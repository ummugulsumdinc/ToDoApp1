using Microsoft.AspNetCore.Mvc; // paket dahil ettik
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces; // Arayüzü kullanmak için dahil ettik
using FluentValidation;

namespace ToDoApp1.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet("hello")] // veriyi okur - listeyi yazdırır
        public IActionResult HelloGet()
        {
            
            var list = _toDoService.GetAll();

            // Sonucu 200 OK durum kodu ile dön
            return Ok(list);
        }

        [HttpGet("hello/{id}")] // Sadece belirli bir ID'yi okur
        public IActionResult HelloGetById(int id)
        {
            var item = _toDoService.GetById(id);

            if (item == null)
            {
                return NotFound("Aradığınız ID'ye ait bir kayıt bulunamadı."); // 404 Not Found 
            }

            // Kayıt bulunduysa 200 OK 
            return Ok(item);
        }

        [HttpPost("hello")] // yeni veri oluşturmak eklemek için
        public IActionResult HelloPost([FromBody] ToDoCreateDto newItem)
        {
            // 4. Servise gitmeden önce gelen veriyi kurallarımızdan geçiriyoruz
            var validationResult = _createValidator.Validate(newItem);

            // 5. Eğer kurallara uymayan bir durum varsa 400 Bad Request dönüyoruz
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            _toDoService.PostAdd(newItem);

            return Ok("Kayıt başarıyla eklendi.");
        }

        [HttpPut("hello/{id}")] // verinin tamamını günceller
        public IActionResult HelloPut(int id, [FromBody] ToDoUpdateDto updatedItem)
        {
            // 6. Güncelleme işlemi için de Update Validator'ı çalıştırıyoruz
            var validationResult = _updateValidator.Validate(updatedItem);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            _toDoService.Update(id,updatedItem);

            return Ok("Kayıt başarıyla güncellendi.");
        }

        [HttpDelete("hello/{id}")] // var olan veriyi siler
        public IActionResult HelloDelete(int id)
        {
            
            _toDoService.Delete(id);

            return Ok("Kayıt başarıyla silindi.");
        }
    }
}