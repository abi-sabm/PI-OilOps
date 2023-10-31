using Microsoft.EntityFrameworkCore;
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

    public async Task<IEnumerable<Work>> GetAllWorks()
    {
        return await _dbContext.Works
            .ToListAsync();
    }

    public async Task<Work> GetWorkById(int id)
    {
        return await _dbContext.Works.FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task AddWork(Work work)
    {
        _dbContext.Works.Add(work);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateWork(Work work)
    {
        _dbContext.Works.Update(work);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteWork(int id)
    {
        var work = _dbContext.Works.FirstOrDefault(w => w.Id == id);
        if (work != null)
        {
            _dbContext.Works.Remove(work);
            await _dbContext.SaveChangesAsync();
        }
    }
}