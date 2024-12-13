using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ResumesController : ControllerBase
{
    private readonly IResumeService _service;

    public ResumesController(IResumeService service)
    {
        _service = service;
    }

    // GET: api/Resumes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var resumes = await _service.GetAllDtosAsync();
        return Ok(resumes);
    }

    // GET: api/Resumes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var resume = await _service.GetDtoByIdAsync(id);
        if (resume == null)
        {
            return NotFound();
        }
        return Ok(resume);
    }

    // POST: api/Resumes
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ResumeBaseDto resumeDto)
    {
        var createdResume = await _service.AddAsync(resumeDto);
        return CreatedAtAction(nameof(GetById), new { id = createdResume.Id }, createdResume);
    }

    // PUT: api/Resumes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ResumeBaseDto resumeDto)
    {
        await _service.UpdateAsync(id, resumeDto);
        return NoContent();
    }

    // DELETE: api/Resumes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
