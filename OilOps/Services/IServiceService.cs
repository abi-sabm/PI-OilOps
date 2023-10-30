using OilOps.Models;

namespace OilOps.Services;

public interface IServiceService
{
    Task<IEnumerable<Service>> GetAllServices();
    Task<Service> GetServiceById(int id);
    Task<IEnumerable<Service>> GetServiceActive(bool status);
    Task AddService(Service service);
    Task UpdateService(Service service);
    Task DeleteService(int id);
}