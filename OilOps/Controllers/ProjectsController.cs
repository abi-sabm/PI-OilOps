using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OilOps.Models;
using OilOps.Models.DTO;
using OilOps.Services;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }
    
    // GET: api/projects
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var projects = await _projectService.GetAllProjects();
        var projectsDto = projects.Select(project => new ProjectDto
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
        var project = _projectService.GetProjectById(id);
        if (project == null) return NotFound();
        return Ok(project);
    }
    
     //GET: api/projects/status
    [HttpGet("status/{status}")]
    [Authorize]
    public async Task<IActionResult> Get(int status)
    {
        var serviceActive = await _projectService.GetProjectByStatus(status);
        if (serviceActive == null) return NotFound();
        return Ok(serviceActive);
    }
    
    // POST: api/projects
    [HttpPost]
    public IActionResult Post(Project project)
    {
        _projectService.AddProject(project);
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }
    
    // PUT: api/projects/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, Project updatedProject)
    {
        var project = _projectService.GetProjectById(id);
        if (project == null) return NotFound();
        project.Name = updatedProject.Name;
        project.Address = updatedProject.Address;
        project.Status = updatedProject.Status;
        _projectService.UpdateProject(project);
        return NoContent();
    }

    // DELETE: api/projects/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var project = _projectService.GetProjectById(id);
        if (project == null) return NotFound();
        _projectService.DeleteProject(id);
        return NoContent();
    }
}