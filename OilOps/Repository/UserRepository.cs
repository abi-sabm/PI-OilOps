using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Repository;

public class UserRepository : IUserRepository
{
    private readonly UsersDbContext _dbContext;

    public UserRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IEnumerable<User> GetAllUsers()
    {
        return _dbContext.Users.ToList();
    }

    public User GetUserById(int id)
    {
        return _dbContext.Users.FirstOrDefault(p => p.Id == id);
    }

    public void AddUser(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        _dbContext.SaveChanges();
    }

    public void DeleteUser(int id)
    {
        var user = _dbContext.Users.FirstOrDefault(p => p.Id == id);
        if (user != null)
        {
            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}