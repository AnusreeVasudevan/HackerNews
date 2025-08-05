using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoriesController : ControllerBase
{
    private readonly IStoryService _service;

    public StoriesController(IStoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<Story>>> Get([FromQuery] int page = 1, [FromQuery] int pageSize = 20, [FromQuery] string? search = null)
    {
        var result = await _service.GetStoriesAsync(page, pageSize, search);
        return Ok(result);
    }
}
