using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers
{
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
            try
            {
                var providedServices = await _service.GetAllDtosAsync();
                return Ok(providedServices);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/ProvidedServices/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var providedService = await _service.GetDtoByIdAsync(id);
                return Ok(providedService);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/ProvidedServices
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProvidedServicesBaseDto providedServiceDto)
        {
            try
            {
                var createdService = await _service.AddAsync(providedServiceDto);
                return Ok(createdService);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/ProvidedServices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProvidedServicesBaseDto providedServiceDto)
        {
            try
            {
                await _service.UpdateAsync(id, providedServiceDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/ProvidedServices/5
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