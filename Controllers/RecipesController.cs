using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipesService _service;

    public RecipesController(IRecipesService service)
    {
        _service = service;
    }

    // GET: api/Recipes
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var recipes = await _service.GetAllDtosAsync();
        return Ok(recipes);
    }

    // GET: api/Recipes/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var recipe = await _service.GetDtoByIdAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return Ok(recipe);
    }

    // POST: api/Recipes
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] RecipesBaseDto recipeDto)
    {
        var createdRecipe = await _service.AddAsync(recipeDto);
        return CreatedAtAction(nameof(GetById), new { id = createdRecipe.Id }, createdRecipe);
    }

    // PUT: api/Recipes/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RecipesBaseDto recipeDto)
    {
        await _service.UpdateAsync(id, recipeDto);
        return NoContent();
    }

    // DELETE: api/Recipes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
