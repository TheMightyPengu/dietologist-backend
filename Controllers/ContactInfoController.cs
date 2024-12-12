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

    // GET: api/ContactInfo
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contactInfos = await _service.GetAllDtosAsync();
        return Ok(contactInfos);
    }

    // GET: api/ContactInfo/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var contactInfo = await _service.GetDtoByIdAsync(id);
        if (contactInfo == null)
        {
            return NotFound(new { Message = $"ContactInfo with ID {id} not found." });
        }
        return Ok(contactInfo);
    }

    // POST: api/ContactInfo
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ContactInfoBaseDto contactInfoDto)
    {
        var createdContactInfo = await _service.AddAsync(contactInfoDto);
        return CreatedAtAction(nameof(GetById), new { id = createdContactInfo.Id }, createdContactInfo);
    }

    // PUT: api/ContactInfo/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContactInfoBaseDto contactInfoDto)
    {
        await _service.UpdateAsync(id, contactInfoDto);
        return NoContent();
    }

    // DELETE: api/ContactInfo/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}