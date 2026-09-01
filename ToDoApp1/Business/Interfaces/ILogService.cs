using ToDoApp1.Business.Dtos.Log;

namespace ToDoApp1.Business.Interfaces
{
    public interface ILogService
    {
        // count: Son kaç log gelsin (default 10)
       
        Task<List<LogResponseDto>> GetLogs(int count = 10, string? level = null);
    }
}