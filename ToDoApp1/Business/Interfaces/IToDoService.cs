using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Business.Interfaces
{
    public interface IToDoService
    {
        
        List<ToDoDto> GetAll();

        ToDoDto? GetById(int id);

        void PutAdd(ToDoDto todo);

        void Update(ToDoDto todo);

        void Delete(int id);
    }
}
