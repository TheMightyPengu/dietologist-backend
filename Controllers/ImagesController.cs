using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImagesController : ControllerBase
{
    private readonly IImagesService _service;

    public ImagesController(IImagesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var images = await _service.GetAllDtosAsync();
        return Ok(images);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var image = await _service.GetDtoByIdAsync(id);
        return Ok(image);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ImagesBaseDto imageDto)
    {
        var createdImage = await _service.AddAsync(imageDto);
        return CreatedAtAction(nameof(GetById), new { id = createdImage.Id }, createdImage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ImagesBaseDto imageDto)
    {
        await _service.UpdateAsync(id, imageDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
