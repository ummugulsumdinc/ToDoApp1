using ToDoApp1.Business.Dtos.Status;

namespace ToDoApp1.Business.Interfaces
{
    public interface IStatusService
    {
        Task<List<StatusResponseDto>> GetAll();
        Task<StatusResponseDto?> GetById(int id);
        Task Add(StatusCreateDto statusDto);
        Task Update(int id, StatusCreateDto statusDto);
        Task Delete(int id);
    }
} 