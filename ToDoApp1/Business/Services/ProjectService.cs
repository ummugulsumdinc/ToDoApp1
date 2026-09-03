using Microsoft.EntityFrameworkCore;
using ToDoApp1.Business.Dtos.Project;
using ToDoApp1.Business.Interfaces;
using ToDoApp1.Data;
using ToDoApp1.Models;

namespace ToDoApp1.Business.Services
{
    public class ProjectService : IProjectService
    {
        private readonly AppDbContext _context;

        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProjectResponseDto>> GetAll()
        {
            var projects = await _context.Projects.ToListAsync();

            var responseList = new List<ProjectResponseDto>();
            foreach (var item in projects)
            {
                responseList.Add(MapToDto(item));
            }
            return responseList;
        }

        public async Task<ProjectResponseDto?> GetById(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                return MapToDto(project);
            }
            return null;
        }

        public async Task Add(ProjectCreateDto projectDto)
        {
            var newProject = new Project
            {
                Title = projectDto.Title,
                Description = projectDto.Description,
                UserId = projectDto.UserId
            };

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();
        }

        public async Task Update(int id, ProjectCreateDto projectDto)
        {
            var targetProject = await _context.Projects.FindAsync(id);
            if (targetProject != null)
            {
                targetProject.Title = projectDto.Title;
                targetProject.Description = projectDto.Description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
            }
        }

        private ProjectResponseDto MapToDto(Project project)
        {
            return new ProjectResponseDto
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description
            };
        }
    }
}