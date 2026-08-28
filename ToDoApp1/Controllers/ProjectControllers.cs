using Microsoft.AspNetCore.Mvc;
using ToDoApp1.Business.Dtos.Project;
using ToDoApp1.Business.Interfaces;

namespace ToDoApp1.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAll();
            return Ok(new
            {
                Message="Projeler başarıyla listelendi",
                Data=projects
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound("Proje bulunamadı.");
            }
            return Ok(new
            {
                Message ="Proje detayı başarıyla getirildi.",
                Data=project 
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectCreateDto projectDto)
        {
            await _projectService.Add(projectDto);
            return Created("",new
            {
                Message="Proje başarıyla eklendi.",
                Data=projectDto
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectCreateDto projectDto)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound("Güncellenecek proje bulunamadı.");
            }

            await _projectService.Update(id, projectDto);
            return Ok(new
            {
                Message = "Project başarıyla güncellendi.",
                Data = project
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound("Silinecek proje bulunamadı.");
            }

            await _projectService.Delete(id);
            return Ok("Proje başarıyla silindi.");
        }
    }
}