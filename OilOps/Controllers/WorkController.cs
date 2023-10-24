using Microsoft.AspNetCore.Mvc;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly IWorkRepository _workRepository;

    public WorkController(IWorkRepository workRepository)
    {
        _workRepository = workRepository;
    }
    
    //GET: api/works
    [HttpGet]
    public IActionResult Get()
    {
        var works = _workRepository.GetAllWorks();
        return Ok(works);
    }

    // GET: api/works/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var work = _workRepository.GetWorkById(id);
        if (work == null) return NotFound();
        return Ok(work);
    }

    // POST: api/works
    [HttpPost]
    public IActionResult Post(Work work)
    {
        _workRepository.AddWork(work);
        return CreatedAtAction(nameof(Get), new { id = work.Id }, work);
    }

    // PUT: api/works/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, Work updatedWork)
    {
        var work = _workRepository.GetWorkById(id);
        if (work == null) return NotFound();
        work.Date = updatedWork.Date;
        work.IdProject = updatedWork.IdProject;
        work.IdServices = updatedWork.IdServices;
        work.HoursService = updatedWork.HoursService;
        work.HourlyRate = updatedWork.HourlyRate;
        work.Value = updatedWork.Value;
        _workRepository.UpdateWork(work);
        return NoContent();
    }

    // DELETE: api/works/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var work = _workRepository.GetWorkById(id);
        if (work == null) return NotFound();
        _workRepository.DeleteWork(id);
        return NoContent();
    }  
}