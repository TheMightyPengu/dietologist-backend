using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProvidedServicesController : ControllerBase
{
    private readonly IProvidedServicesService _service;

    public ProvidedServicesController(IProvidedServicesService service)
    {
        _service = service;
    }

    // GET: api/ProvidedServices
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var providedServices = await _service.GetAllDtosAsync();
        return Ok(providedServices);
    }

    // GET: api/ProvidedServices/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var providedService = await _service.GetDtoByIdAsync(id);
        return Ok(providedService);
    }

    // POST: api/ProvidedServices
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProvidedServicesBaseDto providedServiceDto)
    {
        var createdService = await _service.AddAsync(providedServiceDto);
        return CreatedAtAction(nameof(GetById), new { id = createdService.Id }, createdService);
    }

    // PUT: api/ProvidedServices/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProvidedServicesBaseDto providedServiceDto)
    {
        await _service.UpdateAsync(id, providedServiceDto);
        return NoContent();
    }

    // DELETE: api/ProvidedServices/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}