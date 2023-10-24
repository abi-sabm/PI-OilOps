using Microsoft.EntityFrameworkCore;
using OilOps.Models;
namespace OilOps.DataAccess;

public class ProjectsDbContext : DbContext
{
    public ProjectsDbContext(DbContextOptions<ProjectsDbContext> options) : base(options)
    {
        fillProject();
    }
    
    public DbSet<Project> Projects { get; set; }

    public void fillProject()
    {
        if (Projects.Count() == 0)
        {
            Projects.Add(new Project
            {
                Id = 1,
                Name = "Proyecto1",
                Address = "Calle Falsa 123", 
                Status = 1
            });
        }

        this.SaveChanges();
    }
}