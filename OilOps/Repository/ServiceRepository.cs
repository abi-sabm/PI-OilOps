using Microsoft.EntityFrameworkCore;
using OilOps.DataAccess;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Repository;

public class ServiceRepository : IServiceRepository
{
    private readonly OilOpsDbContext _dbContext;

    public ServiceRepository(OilOpsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<Service>> GetAllServices()
    {
        return await _dbContext.Services
            .ToListAsync();
    }

    public async Task<Service> GetServiceById(int id)
    {
        return await _dbContext.Services.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Service>> GetServiceActive(bool status)
    { 
        return await _dbContext.Services.Where(s => s.Status == status).ToListAsync();
    }
    
    public async Task AddService(Service service)
    {
        _dbContext.Services.Add(service);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateService(Service service)
    {
        _dbContext.Services.Update(service);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteService(int id)
    {
        var service = _dbContext.Services.FirstOrDefault(s => s.Id == id);
        if (service != null)
        {
            _dbContext.Services.Remove(service);
            await _dbContext.SaveChangesAsync();
        }
    }
}