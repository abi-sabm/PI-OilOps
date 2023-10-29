using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Repository;

public class WorkRepository : IWorkRepository
{
    private readonly OilOpsDbContext _dbContext;

    public WorkRepository(OilOpsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Work> GetAllWorks()
    {
        return _dbContext.Works.ToList();
    }

    public Work GetWorkById(int id)
    {
        return _dbContext.Works.FirstOrDefault(p => p.Id == id);
    }

    public void AddWork(Work work)
    {
        _dbContext.Works.Add(work);
        _dbContext.SaveChanges();
    }

    public void UpdateWork(Work work)
    {
        _dbContext.Works.Update(work);
        _dbContext.SaveChanges();
    }

    public void DeleteWork(int id)
    {
        var work = _dbContext.Works.FirstOrDefault(p => p.Id == id);
        if (work != null)
        {
            _dbContext.Works.Remove(work);
            _dbContext.SaveChanges();
        }
    }
}