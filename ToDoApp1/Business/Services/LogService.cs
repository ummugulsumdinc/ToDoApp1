using Microsoft.EntityFrameworkCore;
using ToDoApp1.Business.Dtos.Log;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Data;

namespace ToDoApp1.Business.Services
{
    public class LogService : ILogService
    {
        private readonly AppDbContext _context;

        public LogService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LogResponseDto>> GetLogs(int count = 10, string? level = null)
        {
            var query = _context.Logs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(level))
            {
                query = query.Where(x => x.Level == level);
            }

            var logs = await query
                .OrderByDescending(x => x.Id) // En son eklenen log en üstte 
                .Take(count)                  // Sadece istenen sayı kadarını al
                .ToListAsync();

            var responseList = new List<LogResponseDto>();
            foreach (var item in logs)
            {
                responseList.Add(new LogResponseDto
                {
                    Id = item.Id,
                    Message = item.RenderedMessage,
                    Level = item.Level,
                    Timestamp = item.Timestamp
                });
            }

            return responseList;
        }
    }
}