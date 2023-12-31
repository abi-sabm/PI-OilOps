using OilOps.Models;

namespace OilOps.Repository.Interfaces;

public interface IServiceRepository
{
    Task<IEnumerable<Service>> GetAllServices();
    Task<Service> GetServiceById(int id);
    Task<IEnumerable<Service>> GetServiceActive(bool status);
    Task AddService(Service service);
    Task UpdateService(Service service);
    Task DeleteService(int id);
}