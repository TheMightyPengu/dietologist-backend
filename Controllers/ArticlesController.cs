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

    // GET: api/Articles
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var articles = await _service.GetAllDtosAsync();
        return Ok(articles);
    }

    // GET: api/Articles/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var article = await _service.GetDtoByIdAsync(id);
        if (article == null) return NotFound();
        return Ok(article);
    }

    // POST: api/Articles
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ArticlesBaseDto articleDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdArticle = await _service.AddAsync(articleDto);
        return CreatedAtAction(nameof(GetById), new { id = createdArticle.Id }, createdArticle);
    }

    // PUT: api/Articles/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ArticlesBaseDto articleDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        await _service.UpdateAsync(id, articleDto);
        return NoContent();
    }

    // DELETE: api/Articles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}