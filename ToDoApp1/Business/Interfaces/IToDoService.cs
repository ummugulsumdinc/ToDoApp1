using ToDoApp1.Business.Dtos;

namespace ToDoApp1.Business.Interfaces
{
    public interface IToDoService
    {
        
        List<ToDoResponseDto> GetAll(bool? isCompleted=null, string? sortBy = null);

        ToDoResponseDto? GetById(int id);

        void PostAdd(ToDoCreateDto todo);

        void Update(int id,ToDoUpdateDto todo);

        void Delete(int id);

        void MarkAsComplete(int id);
        List<ToDoResponseDto> Search(string query);
    }
}
