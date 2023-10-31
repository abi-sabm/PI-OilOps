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
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var project = await _projectService.GetProjectById(id);
        if (project == null) return NotFound();
        return Ok(project);
    }
    
    // GET: api/projects/status/{status}
    [HttpGet("status/{status}")]
    [Authorize]
    public async Task<IActionResult> GetProjectsByStatus(int status)
    {
        var projects = await _projectService.GetProjectByStatus(status);
        if (projects == null) return NotFound();
        return Ok(projects);
    }
    
    // POST: api/projects
    [HttpPost]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> Post(Project project)
    {
        await _projectService.AddProject(project);
        return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
    }
    
    // PUT: api/projects/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> Put(int id, Project updatedProject)
    {
        var project = await _projectService.GetProjectById(id);
        if (project == null) return NotFound();
        project.Name = updatedProject.Name;
        project.Address = updatedProject.Address;
        project.Status = updatedProject.Status;
        await _projectService.UpdateProject(project);
        return NoContent();
    }

    // DELETE: api/projects/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> Delete(int id)
    {
        var project = await _projectService.GetProjectById(id);
        if (project == null) return NotFound();
        await _projectService.DeleteProject(id);
        return NoContent();
    }
}