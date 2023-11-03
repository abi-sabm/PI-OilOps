using Microsoft.EntityFrameworkCore;
using OilOpsFront.Models;

namespace OilOpsFront.DataAccess;

public class OilOpsDbContext : DbContext
{
    public OilOpsDbContext(DbContextOptions<OilOpsDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<ServiceModel> Services { get; set; }
    public DbSet<ProjectModel> Projects { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<WorkModel> Works { get; set; }
    
}