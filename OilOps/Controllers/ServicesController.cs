using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OilOps.Models;
using OilOps.Models.DTO;
using OilOps.Services;

namespace OilOps.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceService _serviceService;

    public ServicesController(IServiceService serviceService)
    {
        _serviceService = serviceService;
    }
    
    // GET: api/services
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
    {
        var services = await _serviceService.GetAllServices();
        var servicesDto = services.Select(service => new ServiceDTO
        {
            Name = service.Name,
            Description = service.Description,
            Status = service.Status,
            HourlyRate = service.HourlyRate
        });
        return Ok(servicesDto);
    }
    
    // GET: api/services/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var service = await _serviceService.GetServiceById(id);
        if (service == null) return NotFound();
        return Ok(service);
    }
    
    //GET: api/services/active
    [HttpGet("active/{status}")]
    [Authorize]
    public async Task<IActionResult> Get(bool status)
    {
        var serviceActive = await _serviceService.GetServiceActive(status);
        if (serviceActive == null) return NotFound();
        return Ok(serviceActive);
    }
    
    // POST: api/services
    [HttpPost]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> Post(Service service)
    {
        await _serviceService.AddService(service);
        return CreatedAtAction(nameof(Get), new { id = service.Id }, service);
    }
    
    //PUT: api/services/{id}
    [HttpPut("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> Put(int id, Service updateService)
    {
        var service = await _serviceService.GetServiceById(id);
        if (service == null) return NotFound();
        service.Description = updateService.Description;
        service.Status = updateService.Status;
        service.HourlyRate = updateService.HourlyRate;
        await _serviceService.UpdateService(service);
        return NoContent();
    }
    
    //DELETE: api/services/{id}
    [HttpDelete("{id}")]
    [Authorize(Roles = "1")]
    public async Task<IActionResult> Delete(int id)
    {
        var service = await _serviceService.GetServiceById(id);
        if (service == null) return NotFound();
        _serviceService.DeleteService(id);
        return NoContent();
    }
}