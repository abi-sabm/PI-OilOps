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
    
    public IEnumerable<Service> GetAllServices()
    {
        return _dbContext.Services.ToList();
    }

    public Service GetServiceById(int id)
    {
        return _dbContext.Services.FirstOrDefault(p => p.Id == id);
    }

    public void AddService(Service service)
    {
        _dbContext.Services.Add(service);
        _dbContext.SaveChanges();
    }

    public void UpdateService(Service service)
    {
        _dbContext.Services.Update(service);
        _dbContext.SaveChanges();
    }

    public void DeleteService(int id)
    {
        var service = _dbContext.Services.FirstOrDefault(p => p.Id == id);
        if (service != null)
        {
            _dbContext.Services.Remove(service);
            _dbContext.SaveChanges();
        }
    }
}