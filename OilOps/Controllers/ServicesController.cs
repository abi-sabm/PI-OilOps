using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OilOps.Models;
using OilOps.Repository.Interfaces;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceRepository _serviceRepository;

    public ServicesController(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository;
    }
    
    // GET: api/services
    [HttpGet]
    public IActionResult Get()
    {
        var services = _serviceRepository.GetAllServices();
        return Ok(services);
    }
    
    // GET: api/services/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var service = _serviceRepository.GetServiceById(id);
        if (service == null) return NotFound();
        return Ok(service);
    }
    
    // POST: api/services
    [HttpPost]
    public IActionResult Post(Service service)
    {
        _serviceRepository.AddService(service);
        return CreatedAtAction(nameof(Get), new { id = service.Id }, service);
    }
    
    //PUT: api/services/{id}
    [HttpPut("{id}")]
    public IActionResult Put(int id, Service updateService)
    {
        var service = _serviceRepository.GetServiceById(id);
        if (service == null) return NotFound();
        service.Description = updateService.Description;
        service.Status = updateService.Status;
        service.HourlyRate = updateService.HourlyRate;
        _serviceRepository.UpdateService(service);
        return NoContent();
    }
    
    //DELETE: api/services/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var service = _serviceRepository.GetServiceById(id);
        if (service == null) return NotFound();
        _serviceRepository.DeleteService(id);
        return NoContent();
    }
}