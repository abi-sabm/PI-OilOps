using OilOps.Models;

namespace OilOps.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetAllProjects();
    Task<Project> GetProjectById(int id);
    Task<IEnumerable<Project>> GetProjectByStatus(int status);
    Task AddProject(Project project);
    Task UpdateProject(Project project);
    Task DeleteProject(int id);
}