using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Services;

public class WorkService : IWorkService
{
    private readonly IWorkRepository _workRepository;

    public WorkService(IWorkRepository workRepository)
    {
        _workRepository = workRepository;
    }

    public async Task<IEnumerable<Work>> GetAllWorks()
    {
        return await _workRepository.GetAllWorks();
    }

    public async Task<Work> GetWorkById(int id)
    {
        return await _workRepository.GetWorkById(id);
    }

    public async Task AddWork(Work work)
    {
        await _workRepository.AddWork(work);
    }

    public async Task UpdateWork(Work work)
    {
        await _workRepository.UpdateWork(work);
    }

    public async Task DeleteWork(int id)
    {
        await _workRepository.DeleteWork(id);
    }
}