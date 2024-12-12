using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactInfoController : ControllerBase
{
    private readonly IContactInfoService _service;

    public ContactInfoController(IContactInfoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contactInfos = await _service.GetAllDtosAsync();
        return Ok(contactInfos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contactInfo = await _service.GetDtoByIdAsync(id);
        return Ok(contactInfo);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ContactInfoBaseDto contactInfoDto)
    {
        var createdContactInfo = await _service.AddAsync(contactInfoDto);
        return CreatedAtAction(nameof(GetById), new { id = createdContactInfo.Id }, createdContactInfo);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContactInfoBaseDto contactInfoDto)
    {
        await _service.UpdateAsync(id, contactInfoDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}