using Microsoft.EntityFrameworkCore;
using OilOps.Models;
namespace OilOps.DataAccess;

public class ServicesDbContext : DbContext
{
    public ServicesDbContext(DbContextOptions<ServicesDbContext> options) : base(options)
    {
        fillService();
    }
    
    public DbSet<Service> Services { get; set; }

    public void fillService()
    {
        if (Services.Count() == 0)
        {
            Services.Add(new Service
            {
                Id = 1,
                Description = "Proyecto1",
                Status = true,
                HourlyRate = 3
            });
        }

        this.SaveChanges();
    }
}