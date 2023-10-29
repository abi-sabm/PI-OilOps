using Microsoft.AspNetCore.Mvc;
using OilOps.DTO;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]

public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }
    
    // GET: api/projects
    [HttpGet]
    public IActionResult Get()
    {
        var projects = _projectRepository.GetAllProjects();
        var projectsDto = projects.Select(project => new ProjectDTO
        {
            Name = project.Name,
            Address = project.Address,
            Status = project.Status
        }); 
        
        return Ok(projectsDto);
    }
    
    // GET: api/projects/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var project = _projectRepository.GetProjectById(id);
        if (project == null) return NotFound();
        return Ok(project);
    }
    
    // POST: api/projects
    [HttpPost]
    public IActionResult Post(Project project)
    {
        _projectRepository.AddProject(project);
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }
    
    // PUT: api/projects/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, Project updatedProject)
    {
        var project = _projectRepository.GetProjectById(id);
        if (project == null) return NotFound();
        project.Name = updatedProject.Name;
        project.Address = updatedProject.Address;
        project.Status = updatedProject.Status;
        _projectRepository.UpdateProject(project);
        return NoContent();
    }

    // DELETE: api/projects/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _projectRepository.GetProjectById(id);
        if (project == null) return NotFound();
        _projectRepository.DeleteProject(id);
        return NoContent();
    }
}