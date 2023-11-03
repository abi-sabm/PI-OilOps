using Microsoft.EntityFrameworkCore;
using OilOps.Models;

namespace OilOps.DataAccess;

public class OilOpsDbContext : DbContext
{
    public OilOpsDbContext(DbContextOptions<OilOpsDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Service> Services { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Work> Works { get; set; }

    // Especificar al modelo que las tablas van en singular 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Service>().ToTable("Service");
        modelBuilder.Entity<Project>().ToTable("Project");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Work>().ToTable("Work");
    }
}