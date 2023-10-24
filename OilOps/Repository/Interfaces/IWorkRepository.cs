using OilOps.Models;

namespace OilOps.Repository.Interfaces;

public interface IWorkRepository
{
    IEnumerable<Work> GetAllWorks();
    Work GetWorkById(int id);
    void AddWork(Work work);
    void UpdateWork(Work work);
    void DeleteWork(int id);
}