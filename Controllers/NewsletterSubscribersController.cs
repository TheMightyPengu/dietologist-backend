using dietologist_backend.DTO;
using dietologist_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace dietologist_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NewsletterSubscribersController : ControllerBase
{
    private readonly INewsletterSubscribersService _service;

    public NewsletterSubscribersController(INewsletterSubscribersService service)
    {
        _service = service;
    }

    // GET: api/NewsletterSubscribers
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await _service.GetAllDtosAsync();
        return Ok(subscribers);
    }

    // GET: api/NewsletterSubscribers/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var subscriber = await _service.GetDtoByIdAsync(id);
        if (subscriber == null)
        {
            return NotFound();
        }
        return Ok(subscriber);
    }

    // POST: api/NewsletterSubscribers
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] NewsletterSubscribersBaseDto subscriberDto)
    {
        var createdSubscriber = await _service.AddAsync(subscriberDto);
        return CreatedAtAction(nameof(GetById), new { id = createdSubscriber.Id }, createdSubscriber);
    }

    // PUT: api/NewsletterSubscribers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] NewsletterSubscribersBaseDto subscriberDto)
    {
        await _service.UpdateAsync(id, subscriberDto);
        return NoContent();
    }

    // DELETE: api/NewsletterSubscribers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
