using OilOps.Models;

namespace OilOps.Services;

public interface IWorkService
{
    Task<IEnumerable<Work>> GetAllWorks();
    Task<Work> GetWorkById(int id);
    Task AddWork(Work work);
    Task UpdateWork(Work work);
    Task DeleteWork(int id);
}