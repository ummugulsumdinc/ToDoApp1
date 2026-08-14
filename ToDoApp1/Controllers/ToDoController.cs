using Microsoft.AspNetCore.Mvc;//paket dahil ettik

namespace ToDoApp1.Controllers
{

    public class ToDoItem{
        public int Id { get; set; }
        public string? Title { get; set; }
        public bool IsCompleted { get; set; }
    }
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {

        private static List<ToDoItem> _todo = new List<ToDoItem>
        {
            new ToDoItem{Id=1, Title=".NET öğren ", IsCompleted=true},
            new ToDoItem{Id=2, Title="HTTP çalış", IsCompleted=false}
        };
        [HttpGet("hello")]//veriyi okur - listeyi yazdırır
        public List<ToDoItem> HelloGet()
        {
            return _todo;
        }

        [HttpPost("hello")]//yeni veri oluşturmak eklemek için
        public ToDoItem HelloPost([FromBody] ToDoItem newItem)
        {
            _todo.Add(newItem);
            return newItem;
        }

        [HttpPut("hello/{id}")]//verinin tamamını günceller
        public ToDoItem HelloPut(int id, [FromBody] ToDoItem updatedItem)
        {
            ToDoItem? item = null;
            foreach(var wanted_item in _todo)
            {
                if (wanted_item.Id == id)
                {
                    item = wanted_item;
                    break;
                }
            }
            if (item != null)
            {
                item.Title = updatedItem.Title;
                item.IsCompleted = updatedItem.IsCompleted;
            }

            return item;
        }

        [HttpPatch("hello/{id}")]//verinin bir kısmını günceller
        public ToDoItem? HelloPatch(int id, [FromBody] ToDoItem updatedItem)
        {
            ToDoItem? item = null;

            foreach (var wanted_item in _todo)
            {
                if (wanted_item.Id == id)
                {
                    item = wanted_item;
                    break;
                }
            }

            if (item != null && updatedItem.Title != null)
            {
                item.Title = updatedItem.Title;
            }
            if (item != null && updatedItem.IsCompleted != item.IsCompleted)
            {
                item.IsCompleted = updatedItem.IsCompleted;
            }


            return item;
        }

        [HttpDelete("hello/{id}")]// var olan veriyi siler
        public List<ToDoItem> HelloDelete(int id)
        {
            ToDoItem? item = null;

            foreach (var wanted_item in _todo)
            {
               
                if (wanted_item.Id == id)
                {
                    item=wanted_item; 
                    break;   
                }
            }

            
            if (item != null)
            {
                _todo.Remove(item);
            }

            return _todo;
          
        }

    }
    }
