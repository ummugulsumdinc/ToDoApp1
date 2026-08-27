using Microsoft.EntityFrameworkCore;
using ToDoApp1.Business.Dtos.Status;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Data;
using ToDoApp1.Models;

namespace ToDoApp1.Business.Services
{
    public class StatusService : IStatusService
    {
        private readonly AppDbContext _context;

        public StatusService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<StatusResponseDto>> GetAll()
        {
            var statuses = await _context.Statuses.ToListAsync();

            var responseList = new List<StatusResponseDto>();
            foreach (var item in statuses)
            {
                responseList.Add(MapToDto(item));
            }
            return responseList;
        }

        public async Task<StatusResponseDto?> GetById(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                return MapToDto(status);
            }
            return null;
        }

        public async Task Add(StatusCreateDto statusDto)
        {
            var newStatus = new Status
            {
                Name = statusDto.Name
            };

            _context.Statuses.Add(newStatus);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, StatusCreateDto statusDto)
        {
            var targetStatus = await _context.Statuses.FindAsync(id);
            if (targetStatus != null)
            {
                targetStatus.Name = statusDto.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status != null)
            {
                _context.Statuses.Remove(status);
                await _context.SaveChangesAsync();
            }
        }

        private StatusResponseDto MapToDto(Status status)
        {
            return new StatusResponseDto
            {
                Id = status.Id,
                Name = status.Name
            };
        }
    }
}