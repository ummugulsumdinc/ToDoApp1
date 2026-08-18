using Microsoft.AspNetCore.Http.HttpResults;
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Models;

namespace ToDoApp1.Business.Services
{
   
    public class ToDoService: IToDoService
    {
        private readonly List<ToDo> _todoList;
        private int _nextid = 1;
        public ToDoService()//constructor
        {
            _todoList= new List<ToDo>();
        }
        public List<ToDoResponseDto> GetAll()
        {
            var responseList = new List<ToDoResponseDto>(); // Yeni boş bir response listesi oluştur
            foreach (var item in _todoList) // Asıl verileri tek tek gez
            {
                responseList.Add(new ToDoResponseDto // itemi dtoya kopyalayıp responseliste aktarıyor
                {
                    Id = item.Id,
                    Title = item.Title,
                    IsCompleted = item.IsCompleted,
                    CreatedDate = item.CreatedDate?.ToString("dd.MM.yyyy HH:mm"),
                    UpdatedDate = item.UpdatedDate?.ToString("dd.MM.yyyy HH:mm")
                });
            }
            return responseList; // response DTO listesini döndür
        }

        public ToDoResponseDto? GetById(int id)
        {
            foreach(var item in _todoList)
            {
                if (item.Id == id)
                {
                    return new ToDoResponseDto // Bulunan Modeli DTO'ya çevir
                    {
                        Id = item.Id,
                        Title = item.Title,
                        IsCompleted = item.IsCompleted,
                        CreatedDate = item.CreatedDate?.ToString("dd.MM.yyyy HH:mm"),
                        UpdatedDate= item.UpdatedDate?.ToString("dd.MM.yyyy HH:mm"),
                    };
                }
            }
            return null;
        }

        public void PostAdd(ToDoCreateDto todo)
        {
            var newTodo = new ToDo // DTO'daki bilgileri kullanarak asıl Modeli (ToDo) oluşturuyoruz
            {
                Id = _nextid,
                Title = todo.Title, // Kullanıcının gönderdiği başlığı aldık
                IsCompleted = false, // Yeni kayıt varsayılan olarak tamamlanmamıştır
                CreatedDate = DateTime.Now
            };

            _nextid++;
            _todoList.Add(newTodo); //TODO MODEL LİSTESİNİ RETURN EDİYORUZ
        }

        public void Update(int id, ToDoUpdateDto todo)
        {
            ToDo? targetitem = null;


            foreach (var item in _todoList)
            {
                if (item.Id == id)
                {
                    targetitem = item;// aynı adresi işaret ediyolar itemin adresini
                    break;
                }
            }
            if(targetitem != null)
            {
               targetitem.Title= todo.Title;
               targetitem.IsCompleted= todo.IsCompleted;
               targetitem.UpdatedDate = DateTime.Now;
            }
        }
        public void Delete(int id)
        {
            ToDo? itemToDelete = null;
            foreach (var item in _todoList)
            {
                if (item.Id == id)
                {
                    itemToDelete = item;
                    break;
                }
            }
            if (itemToDelete != null)
            {
                _todoList.Remove(itemToDelete);
            }
        }
    }
}
