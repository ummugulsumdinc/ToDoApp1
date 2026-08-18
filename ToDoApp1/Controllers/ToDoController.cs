using Microsoft.AspNetCore.Mvc; // paket dahil ettik
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces; // Arayüzü kullanmak için dahil ettik

namespace ToDoApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        // Dependency Injection: Asıl işi yapacak servisi içeri alıyoruz.
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
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
           
            _toDoService.PostAdd(newItem);

            return Ok("Kayıt başarıyla eklendi.");
        }

        [HttpPut("hello/{id}")] // verinin tamamını günceller
        public IActionResult HelloPut(int id, [FromBody] ToDoUpdateDto updatedItem)
        {
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