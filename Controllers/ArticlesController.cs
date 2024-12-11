using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace dietologist_backend.Controllers
{
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
            try
            {
                var articles = await _service.GetAllDtosAsync();
                return Ok(articles);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var article = await _service.GetDtoByIdAsync(id);
                if (article == null) return NotFound();
                return Ok(article);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // POST: api/Articles
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ArticlesBaseDto articleDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var createdArticle = await _service.AddAsync(articleDto);
                return CreatedAtAction(nameof(GetById), new { id = createdArticle.Id }, createdArticle);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // PUT: api/Articles/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ArticlesBaseDto articleDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _service.UpdateAsync(id, articleDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        private IActionResult HandleException(Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}\n{ex.StackTrace}");

            return StatusCode(500, new
            {
                Error = "An unexpected error occurred. Please try again later.",
                Details = ex.Message
            });
        }
    }
}
