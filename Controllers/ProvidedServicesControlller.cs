using dietologist_backend.Models;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProvidedServicesController(IProvidedServicesService service) : ControllerBase
    {
        // GET: /ProvidedServices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProvidedServices>>> GetAll()
        {
            var providedServices = await service.GetAllAsync();
            return Ok(providedServices);
        }

        // GET: /ProvidedServices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProvidedServices>> GetById(int id)
        {
            var providedService = await service.GetByIdAsync(id);
            if (providedService == null)
            {
                return NotFound();
            }
            return Ok(providedService);
        }

        // POST: /ProvidedServices
        [HttpPost]
        public async Task<ActionResult<ProvidedServices>> Add([FromBody] ProvidedServices? providedService)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdService = await service.AddAsync(providedService);
            return CreatedAtAction(nameof(GetById), new { id = createdService.Id }, createdService);
        }

        // PUT: /ProvidedServices/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProvidedServices providedService)
        {
            if (id != providedService.Id)
            {
                return BadRequest("ID mismatch");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingService = await service.GetByIdAsync(id);
            if (existingService == null)
            {
                return NotFound();
            }
            await service.UpdateAsync(providedService);
            return NoContent();
        }

        // DELETE: /ProvidedServices/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingService = await service.GetByIdAsync(id);
            if (existingService == null)
            {
                return NotFound();
            }
            await service.DeleteAsync(id);
            return NoContent();
        }
    }
}