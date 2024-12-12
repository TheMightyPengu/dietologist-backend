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

    // GET: api/Appointments
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var appointments = await _service.GetAllDtosAsync();
        return Ok(appointments);
    }

    // GET: api/Appointments/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var appointment = await _service.GetDtoByIdAsync(id);
        return Ok(appointment);
    }

    // POST: api/Appointments
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AppointmentsBaseDto appointmentDto)
    {
        var createdAppointment = await _service.AddAsync(appointmentDto);
        return Ok(createdAppointment);
    }

    // PUT: api/Appointments/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] AppointmentsBaseDto appointmentDto)
    {
        await _service.UpdateAsync(id, appointmentDto);
        return NoContent();
    }

    // DELETE: api/Appointments/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}