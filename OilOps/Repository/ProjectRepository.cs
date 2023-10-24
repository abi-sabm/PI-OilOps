using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly ProjectsDbContext _dbContext;

    public ProjectRepository(ProjectsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Project> GetAllProjects()
    {
        return _dbContext.Projects.ToList();
    }

    public Project GetProjectById(int id)
    {
        return _dbContext.Projects.FirstOrDefault(p => p.Id == id);
    }

    public void AddProject(Project project)
    {
        _dbContext.Projects.Add(project);
        _dbContext.SaveChanges();
    }

    public void UpdateProject(Project project)
    {
        _dbContext.Projects.Update(project);
        _dbContext.SaveChanges();
    }
    
    public void DeleteProject(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);
        if (project != null)
        {
            _dbContext.Projects.Remove(project);
            _dbContext.SaveChanges();  
        }
    }
}