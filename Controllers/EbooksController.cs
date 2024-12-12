using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

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
        var ebooks = await _service.GetAllDtosAsync();
        return Ok(ebooks);
    }

    // GET: api/Ebooks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ebook = await _service.GetDtoByIdAsync(id);
        if (ebook == null)
        {
            return NotFound(new { Message = $"Ebook with ID {id} not found." });
        }
        return Ok(ebook);
    }

    // POST: api/Ebooks
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] EbooksBaseDto ebookDto)
    {
        var createdEbook = await _service.AddAsync(ebookDto);
        return CreatedAtAction(nameof(GetById), new { id = createdEbook.Id }, createdEbook);
    }

    // PUT: api/Ebooks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EbooksBaseDto ebookDto)
    {
        await _service.UpdateAsync(id, ebookDto);
        return NoContent();
    }

    // DELETE: api/Ebooks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}