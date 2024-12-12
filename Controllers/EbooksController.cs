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

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ebooks = await _service.GetAllDtosAsync();
        return Ok(ebooks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ebook = await _service.GetDtoByIdAsync(id);
        return Ok(ebook);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] EbooksBaseDto ebookDto)
    {
        var createdEbook = await _service.AddAsync(ebookDto);
        return CreatedAtAction(nameof(GetById), new { id = createdEbook.Id }, createdEbook);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] EbooksBaseDto ebookDto)
    {
        await _service.UpdateAsync(id, ebookDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}