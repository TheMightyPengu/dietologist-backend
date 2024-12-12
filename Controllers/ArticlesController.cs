using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly IArticlesService _service;

    public ArticlesController(IArticlesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _service.GetAllDtosAsync();
        return Ok(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _service.GetDtoByIdAsync(id);
        return Ok(article);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ArticlesBaseDto articleDto)
    {
        var createdArticle = await _service.AddAsync(articleDto);
        return CreatedAtAction(nameof(GetById), new { id = createdArticle.Id }, createdArticle);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ArticlesBaseDto articleDto)
    {
        await _service.UpdateAsync(id, articleDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}