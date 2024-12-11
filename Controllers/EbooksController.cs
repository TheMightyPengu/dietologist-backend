using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbooksController : ControllerBase
    {
        private readonly IEbooksService _service;

        public EbooksController(IEbooksService service)
        {
            _service = service;
        }

        // GET: api/Ebooks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var ebooks = await _service.GetAllDtosAsync();
                return Ok(ebooks);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/Ebooks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ebook = await _service.GetDtoByIdAsync(id);
                if (ebook == null)
                {
                    return NotFound(new { Message = $"Ebook with ID {id} not found." });
                }
                return Ok(ebook);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/Ebooks
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EbooksBaseDto ebookDto)
        {
            try
            {
                var createdEbook = await _service.AddAsync(ebookDto);
                return Ok(createdEbook);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/Ebooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EbooksBaseDto ebookDto)
        {
            try
            {
                await _service.UpdateAsync(id, ebookDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/Ebooks/5
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
