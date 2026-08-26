using Microsoft.EntityFrameworkCore;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Models;
using FluentValidation;
using ToDoApp1.Data;
using ToDoApp1.Business.Dtos.ToDo;

namespace ToDoApp1.Business.Services
{
   
    public class ToDoService: IToDoService
    {
        private readonly AppDbContext _context;//databaseden alıyoruz artık     
        public ToDoService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<List<ToDoResponseDto>> GetAll(int? statusId = null, string? sortBy = null, int pageNumber = 1, int pageSize = 3)
        {
            var query = _context.ToDos
                        .Include(x => x.Project)
                        .Include(x => x.Status)
                        .Include(x => x.User)
                        .AsQueryable();

            if (statusId.HasValue)
            {
                query = query.Where(x => x.StatusId == statusId.Value);
            }

            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                var sortTerm = sortBy.ToLower();
                if (sortTerm == "title")
                {
                    query = query.OrderBy(x => x.Title);
                }
                else if (sortTerm == "createdate")
                {
                    query = query.OrderBy(x => x.CreatedDate);
                }
                else if (sortTerm == "duedate")
                {
                    query = query.OrderBy(x => x.DueDate);
                }
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }
            // Skip -> Geçilecek kayıt sayısı = (Sayfa Numarası - 1) * Sayfadaki Veri Sayısı
            // Take -> Alınacak kayıt sayısı = Sayfadaki Veri Sayısı (Biz 3 olarak belirledik)

            var list = await query
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();

            var responseList = new List<ToDoResponseDto>();
            foreach (var item in list)
            {
                responseList.Add(MapToDto(item));
            }

            return responseList;
        }

        public async Task<ToDoResponseDto?> GetById(int id)
        {
            // FirstOrDefaultAsync veya FindAsync ile veritabanından tek kayıt çekiyoruz
            var item = await _context.ToDos
                .Include(x => x.Project)
                .Include(x => x.Status)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (item != null)
            {
                return MapToDto(item);
            }
            return null;
        }

        public async Task<ToDoResponseDto> PostAdd(ToDoCreateDto todo)
        {
            var newTodo = new ToDo
            {
                Title = todo.Title,
                Description = todo.Description,
                CreatedDate = DateTime.Now,
                DueDate = todo.DueDate,
                Priority = todo.Priority,
                ProjectId = todo.ProjectId,
                StatusId = todo.StatusId ?? 1, // Null gelirse varsayılan 1
                UserId = todo.UserId
            };

            _context.ToDos.Add(newTodo);
            await _context.SaveChangesAsync();

            // DÜZELTME: Manuel nesne yerine MapToDto kullanan GetById çağrıldı, böylece isimler eksiksiz döner
            return await GetById(newTodo.Id);
        }

        public async Task Update(int id, ToDoUpdateDto todo)
        {
            var targetitem = await _context.ToDos.FindAsync(id);

            if (targetitem != null)
            {
                targetitem.Title = todo.Title;
                targetitem.Description = todo.Description;
                targetitem.UpdatedDate = DateTime.Now;
                targetitem.DueDate = todo.DueDate;
                targetitem.Priority = todo.Priority;

                // DÜZELTME: SaveChanges sadece kayıt bulunduysa çalışacak şekilde if bloğunun içine alındı
                await _context.SaveChangesAsync();
            }
        }
        public async Task Delete(int id)
        {
            var itemToDelete = await _context.ToDos.FindAsync(id);

            if (itemToDelete != null)
            {
                _context.ToDos.Remove(itemToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task MarkAsComplete(int id)
        {
            var targetItem = await _context.ToDos.FindAsync(id);
            if (targetItem != null)
            {
                targetItem.StatusId = 3; // "3" = Tamamlandı varsayıyoruz
                targetItem.UpdatedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ToDoResponseDto>> Search(string query)
        {
            var lowerQuery = query.ToLower();

            // databasede ToDos klasörüne bak await ile donmayı önlüyoruz ve gelen verileri listte tutuyoruz
            var list = await _context.ToDos
                .Include(x => x.Project)
                .Include(x => x.Status)
                .Include(x => x.User)
                .Where(x =>
                    (x.Title != null && x.Title.ToLower().Contains(lowerQuery)) ||
                    (x.Description != null && x.Description.ToLower().Contains(lowerQuery)))
                .ToListAsync();
            var responseList = new List<ToDoResponseDto>();
            foreach (var item in list)
            {
                responseList.Add(MapToDto(item));
            }

            return responseList;
        }
        
        private ToDoResponseDto MapToDto(ToDo item)// sürekli dto yazmamak için 
        {
            return new ToDoResponseDto
            {
               
                Title = item.Title,
                Description = item.Description,
                Id= item.Id,
                CreatedDate = item.CreatedDate?.ToString("dd/MM/yyyy HH:mm"),
                UpdatedDate = item.UpdatedDate?.ToString("dd/MM/yyyy HH:mm"),
                DueDate = item.DueDate?.ToString("dd/MM/yyyy HH:mm"),
                Priority = item.Priority,
                StatusId = item.StatusId,
                StatusName= item.Status?.Name,
                ProjectId = item.ProjectId,
                ProjectTitle=item.Project?.Title,
                UserId=item.UserId,
                UserName=item.User?.Name,
                UserSurname=item.User?.Surname,

               
            };
        }
    }
}
