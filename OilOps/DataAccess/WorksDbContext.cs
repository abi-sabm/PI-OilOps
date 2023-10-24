using Microsoft.EntityFrameworkCore;
using OilOps.Models;

namespace OilOps.DataAccess;

public class WorksDbContext : DbContext
{
    public WorksDbContext(DbContextOptions<WorksDbContext> options) : base(options)
    {
        fillWork();
    }
    
    public DbSet<Work> Works { get; set; }

    public void fillWork()
    {
        if (Works.Count() == 0)
        {
            Works.Add(new Work
            {
                Id = 1,
                Date = new DateOnly(),
                IdProject = 1,
                IdServices = 1,
                HoursService = 5,
                HourlyRate = 6,
                Value = 1
            });
        }

        this.SaveChanges();
    }
}