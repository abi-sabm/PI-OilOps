using Microsoft.EntityFrameworkCore;
using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Repository;

public class UserRepository : IUserRepository
{
    private readonly OilOpsDbContext _dbContext;

    public UserRepository(OilOpsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _dbContext.Users
            .ToListAsync();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task AddUser(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUser(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();
        }
    }
    
    // login -> userSession para almacenar el login
  /*  public async Task<Login> UserSession(string userName, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password);
        return user;
    }*/
}