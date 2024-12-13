using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SocialMediaLinksController : ControllerBase
{
    private readonly ISocialMediaLinksService _service;

    public SocialMediaLinksController(ISocialMediaLinksService service)
    {
        _service = service;
    }

    // GET: api/SocialMediaLinks
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var links = await _service.GetAllDtosAsync();
        return Ok(links);
    }

    // GET: api/SocialMediaLinks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var link = await _service.GetDtoByIdAsync(id);
        if (link == null)
        {
            return NotFound();
        }
        return Ok(link);
    }

    // POST: api/SocialMediaLinks
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] SocialMediaLinksBaseDto linkDto)
    {
        var createdLink = await _service.AddAsync(linkDto);
        return CreatedAtAction(nameof(GetById), new { id = createdLink.Id }, createdLink);
    }

    // PUT: api/SocialMediaLinks/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SocialMediaLinksBaseDto linkDto)
    {
        await _service.UpdateAsync(id, linkDto);
        return NoContent();
    }

    // DELETE: api/SocialMediaLinks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
