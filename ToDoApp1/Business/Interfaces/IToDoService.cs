using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Business.Interfaces
{
    public interface IToDoService
    {
        
        List<ToDoResponseDto> GetAll();

        ToDoResponseDto? GetById(int id);

        void PostAdd(ToDoCreateDto todo);

        void Update(int id,ToDoUpdateDto todo);

        void Delete(int id);
    }
}
