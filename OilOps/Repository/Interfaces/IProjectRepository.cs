using OilOps.Models;

namespace OilOps.Repository.Interfaces;

public interface IProjectRepository
{
    IEnumerable<Project> GetAllProjects();
    Project GetProjectById(int id);
    void AddProject(Project project);
    void UpdateProject(Project project);
    void DeleteProject(int id);
}