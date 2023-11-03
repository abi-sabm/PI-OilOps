using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OilOps.Models;
using OilOps.Models.DTO;
using OilOps.Repository.Interfaces;
using OilOps.Services;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkController : ControllerBase
{
    private readonly IWorkService _workService;

    public WorkController(IWorkService workService)
    {
        _workService = workService;
    }
    
    //GET: api/works
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var works = await _workService.GetAllWorks();
        var worksDto = works.Select(work => new WorkDTO
        {
            Date = work.Date,
            HoursService = work.HoursService,
            HourlyRate = work.HourlyRate,
            Value = work.Value,
            IdProject = work.IdProject,
            IdService = work.IdService
        });
        return Ok(worksDto);
    }

    // GET: api/works/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var work = await _workService.GetWorkById(id);
        if (work == null) return NotFound();
        return Ok(work);
    }

    // POST: api/works
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Post(Work work)
    {
        await _workService.AddWork(work);
        return CreatedAtAction(nameof(Get), new { id = work.Id }, work);
    }

    // PUT: api/works/{id}
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> Put(int id, Work updatedWork)
    {
        var work = await _workService.GetWorkById(id);
        if (work == null) return NotFound();
        work.Date = updatedWork.Date;
        work.HoursService = updatedWork.HoursService;
        work.HourlyRate = updatedWork.HourlyRate;
        work.Value = updatedWork.Value;
        work.IdProject = updatedWork.IdProject;
        work.IdService = updatedWork.IdService;
        await _workService.UpdateWork(work);
        return NoContent();
    }

    // DELETE: api/works/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var work = await _workService.GetWorkById(id);
        if (work == null) return NotFound();
        await _workService.DeleteWork(id);
        return NoContent();
    }  
}