using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMessagesController : ControllerBase
    {
        private readonly IContactMessagesService _service;

        public ContactMessagesController(IContactMessagesService service)
        {
            _service = service;
        }

        // GET: api/ContactMessages
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var messages = await _service.GetAllDtosAsync();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/ContactMessages/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var message = await _service.GetDtoByIdAsync(id);
                if (message == null)
                {
                    return NotFound(new { Message = $"ContactMessage with ID {id} not found." });
                }
                return Ok(message);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/ContactMessages
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ContactMessagesBaseDto messageDto)
        {
            try
            {
                var createdMessage = await _service.AddAsync(messageDto);
                return Ok(createdMessage);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/ContactMessages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ContactMessagesBaseDto messageDto)
        {
            try
            {
                await _service.UpdateAsync(id, messageDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/ContactMessages/5
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
