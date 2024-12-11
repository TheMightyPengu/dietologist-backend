using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {
        private readonly IContactInfoService _service;

        public ContactInfoController(IContactInfoService service)
        {
            _service = service;
        }

        // GET: api/ContactInfo
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var contactInfos = await _service.GetAllDtosAsync();
                return Ok(contactInfos);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/ContactInfo/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var contactInfo = await _service.GetDtoByIdAsync(id);
                if (contactInfo == null)
                {
                    return NotFound(new { Message = $"ContactInfo with ID {id} not found." });
                }

                return Ok(contactInfo);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/ContactInfo
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContactInfoBaseDto contactInfoDto)
        {
            try
            {
                var createdContactInfo = await _service.AddAsync(contactInfoDto);
                return CreatedAtAction(nameof(GetById), new { id = createdContactInfo.Id }, createdContactInfo);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/ContactInfo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactInfoBaseDto contactInfoDto)
        {
            try
            {
                await _service.UpdateAsync(id, contactInfoDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/ContactInfo/5
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
