using ToDoApp1.Business.Dtos.ToDo;

namespace ToDoApp1.Business.Interfaces
{
    public interface IToDoService
    {
        Task<List<ToDoResponseDto>> GetAll(int? statusId = null, string? sortBy = null,int pageNumber = 1, int pageSize = 3);
        Task<ToDoResponseDto?> GetById(int id);
        Task<ToDoResponseDto> PostAdd(ToDoCreateDto todo);
        Task Update(int id, ToDoUpdateDto todo);
        Task Delete(int id);
        Task MarkAsComplete(int id);
        Task<List<ToDoResponseDto>> Search(string query);
    }
}