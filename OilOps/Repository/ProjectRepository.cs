using Microsoft.EntityFrameworkCore;
using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly OilOpsDbContext _dbContext;

    public ProjectRepository(OilOpsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Project>> GetAllProjects()
    {
        return await _dbContext.Projects
            .ToListAsync();
    }

    public async Task<Project> GetProjectById(int id)
    {
        return await _dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Project>> GetProjectByStatus(int status)
    {
        return await _dbContext.Projects.Where(p => p.Status == status).ToListAsync();
    }
    
    public async Task AddProject(Project project)
    {
        _dbContext.Projects.Add(project);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateProject(Project project)
    {
        _dbContext.Projects.Update(project);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteProject(int id)
    {
        var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);
        if (project != null)
        {
            _dbContext.Projects.Remove(project);
            await _dbContext.SaveChangesAsync();  
        }
    }
}