using Microsoft.EntityFrameworkCore;
using OilOps.Models;
namespace OilOps.DataAccess;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
        fillUser();
    }
    
    public DbSet<User> Users { get; set; }

    public void fillUser()
    {
        if (Users.Count() == 0)
        {
            Users.Add(new User
            {
                Id = 1,
                Name = "Name1",
                DNI = 12345678,
                UserType = 1,
                Password = "asdf"
            });
        }

        this.SaveChanges();
    }
}