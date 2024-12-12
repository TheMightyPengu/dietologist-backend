using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentsService _service;

    public AppointmentsController(IAppointmentsService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _service.GetAllDtosAsync();
        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _service.GetDtoByIdAsync(id);
        return Ok(appointment);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AppointmentsBaseDto appointmentDto)
    {
        var createdAppointment = await _service.AddAsync(appointmentDto);
        return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AppointmentsBaseDto appointmentDto)
    {
        await _service.UpdateAsync(id, appointmentDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}