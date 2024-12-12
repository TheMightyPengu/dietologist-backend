using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactMessagesController : ControllerBase
{
    private readonly IContactMessagesService _service;

    public ContactMessagesController(IContactMessagesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var messages = await _service.GetAllDtosAsync();
        return Ok(messages);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var message = await _service.GetDtoByIdAsync(id);
        return Ok(message);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ContactMessagesBaseDto messageDto)
    {
        var createdMessage = await _service.AddAsync(messageDto);
        return CreatedAtAction(nameof(GetById), new { id = createdMessage.Id }, createdMessage);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContactMessagesBaseDto messageDto)
    {
        await _service.UpdateAsync(id, messageDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}