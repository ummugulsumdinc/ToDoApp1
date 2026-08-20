using Microsoft.AspNetCore.Http.HttpResults;
using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Models;
using FluentValidation;
using System.Reflection.Metadata;

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
        public List<ToDoResponseDto> GetAll(bool? isCompleted = null, string? sortBy = null)
        {
            var filteredList = _todoList.AsEnumerable();

            if (isCompleted.HasValue)
            {
                // Burada .ToList() dememize gerek yok,  sorgu aşamasındayız
                filteredList = filteredList.Where(x => x.IsCompleted == isCompleted.Value);
            }

            if(!string.IsNullOrWhiteSpace(sortBy))
            {
                var sortTerm = sortBy.ToLower();
                if (sortTerm == "title")
                {
                    filteredList = filteredList.OrderBy(x => x.Title);// ascending order depending on title
                }else if(sortTerm == "createdate")
                {
                    filteredList = filteredList.OrderBy(x => x.CreatedDate);
                }
                else if (sortTerm == "duedate")
                {
                    filteredList = filteredList.OrderBy(x => x.DueDate);
                }
            }

            var responseList = new List<ToDoResponseDto>();
            foreach (var item in filteredList) {

                responseList.Add(MapToDto(item));
            }
            return responseList;
        }

        public ToDoResponseDto? GetById(int id)
        {
            foreach(var item in _todoList)
            {
                if (item.Id == id)
                {
                    return MapToDto(item);
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
                Description= todo.Description,
                IsCompleted = false, // Yeni kayıt varsayılan olarak tamamlanmamıştır
                CreatedDate = DateTime.Now,
                DueDate = todo.DueDate,
                Priority = todo.Priority
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
               targetitem.Description= todo.Description;
               targetitem.IsCompleted= todo.IsCompleted;
               targetitem.UpdatedDate = DateTime.Now;
               targetitem.DueDate = todo.DueDate;
               targetitem.Priority= todo.Priority;
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

        public void MarkAsComplete(int id)//FOR MARK AS COMPLETED
        {
            var targetItem = _todoList.FirstOrDefault(x => x.Id == id);
            if (targetItem != null)
            {
                targetItem.IsCompleted= true;
                targetItem.UpdatedDate = DateTime.Now;
            }
        }

        public List<ToDoResponseDto> Search(string query)
        {
            // Gelen metni küçük harfe çeviriyoruz ki büyük/küçük harf duyarlılığı olmasın
            var lowerQuery = query.ToLower();

            var filteredList = _todoList.Where(x =>
                (x.Title != null && x.Title.ToLower().Contains(lowerQuery)) ||
                (x.Description != null && x.Description.ToLower().Contains(lowerQuery))
            );

            // İstenen stringe sahip olanlar dtoya çevirip listeye ekliyoruz
            var responseList = new List<ToDoResponseDto>();
            foreach (var item in filteredList)
            {
                responseList.Add(MapToDto(item));
            }

            return responseList;
        }
        private ToDoResponseDto MapToDto(ToDo item)// sürekli dto yazmamak için 
        {
            return new ToDoResponseDto
            {
                Id = item.Id,
                Title = item.Title,
                Description = item.Description,
                IsCompleted = item.IsCompleted,
                CreatedDate = item.CreatedDate?.ToString("dd/MM/yyyy HH:mm"),
                UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy HH:mm"),
                DueDate = item.DueDate?.ToString("dd/MM/yyyy HH:mm"),
                Priority = item.Priority
            };
        }
    }
}
