using OilOps.Models;

namespace OilOps.Repository.Interfaces;

public interface IServiceRepository
{
    IEnumerable<Service> GetAllServices();
    Service GetServiceById(int id);
    //Service GetServiceByStatus(bool status);
    void AddService(Service service);
    void UpdateService(Service service);
    void DeleteService(int id);
}