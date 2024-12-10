using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers
{
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
            try
            {
                var appointments = await _service.GetAllDtosAsync();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/Appointments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var appointment = await _service.GetDtoByIdAsync(id);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/Appointments
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AppointmentsBaseDto appointmentDto)
        {
            try
            {
                var createdAppointment = await _service.AddAsync(appointmentDto);
                return Ok(createdAppointment);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/Appointments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentsBaseDto appointmentDto)
        {
            try
            {
                await _service.UpdateAsync(id, appointmentDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/Appointments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}\n{ex.StackTrace}");

            return StatusCode(500, new
            {
                Error = "An unexpected error occurred. Please try again later.",
                Details = ex.Message
            });
        }
    }
}
