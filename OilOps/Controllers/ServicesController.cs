using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using OilOps.DTO;
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
        var services  =_serviceRepository.GetAllServices() ;
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
    public IActionResult Get(int id)
    {
        var service = _serviceRepository.GetServiceById(id);
        if (service == null) return NotFound();
        return Ok(service);
    }
    
    // GET: api/services/active
    //[HttpGet("active")]
    //public IActionResult Get(bool status)
    //{
    //    var serviceActive = _serviceRepository.//GetServiceByStatus(status);
    //    if (serviceActive == null) return NotFound();
    //    return Ok(serviceActive);
    //}
    
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