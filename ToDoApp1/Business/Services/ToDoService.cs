using ToDoApp1.Business.Dtos;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Business.Services
{
   
    public class ToDoService: IToDoService
    {
        private readonly List<ToDoDto> _todoList;
        private int _nextid = 1;
        public ToDoService()//constructor
        {
            _todoList= new List<ToDoDto>();
        }
        public List<ToDoDto> GetAll()
        {
            return _todoList;
        }

        public ToDoDto? GetById(int id)
        {
            foreach(var item in _todoList)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public void PutAdd(ToDoDto todo)
        {
            todo.Id = _nextid;
            _nextid++;
            _todoList.Add(todo);//listelere ekleme yapmak için sistemde hazırda bulunan fonksiyon
        }

        public void Update(ToDoDto todo)
        {
            ToDoDto? targetitem = null;

            foreach (var item in _todoList)
            {
                if (item.Id == todo.Id)
                {
                    targetitem = item;// aynı adresi işaret ediyolar itemin adresini
                    break;
                }
            }
            if(targetitem != null)
            {
               targetitem.Title= todo.Title;
                targetitem.IsCompleted= todo.IsCompleted;
            }
        }
        public void Delete(int id)
        {
            ToDoDto? itemToDelete = null;
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
