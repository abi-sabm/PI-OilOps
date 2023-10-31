using Azure.Identity;
using OilOps.Models;

namespace OilOps.Repository.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task AddUser(User user);
    Task UpdateUser(User user);
    Task DeleteUser(int id);
    /*Task<Login> UserSession(string userName, string password);*/
}