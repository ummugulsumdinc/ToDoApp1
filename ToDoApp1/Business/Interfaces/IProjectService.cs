using ToDoApp1.Business.Dtos.Project;

namespace ToDoApp1.Business.Interfaces
{
    public interface IProjectService
    {
        Task<List<ProjectResponseDto>> GetAll();
        Task<ProjectResponseDto?> GetById(int id);
        Task Add(ProjectCreateDto projectDto);
        Task Update(int id, ProjectCreateDto projectDto);
        Task Delete(int id);
    }
}
