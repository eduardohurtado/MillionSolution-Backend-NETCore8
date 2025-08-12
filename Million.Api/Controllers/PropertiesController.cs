using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PropertiesController : ControllerBase
{
    private readonly PropertyService _service;

    public PropertiesController(PropertyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] string? name,
        [FromQuery] string? address,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int skip = 0,
        [FromQuery] int limit = 50)
    {
        var filter = new PropertyFilterDto
        {
            Name = name,
            Address = address,
            MinPrice = minPrice,
            MaxPrice = maxPrice,
            Skip = skip,
            Limit = Math.Min(limit, 200)
        };

        var result = await _service.GetAsync(filter);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var prop = await _service.GetByIdAsync(id);
        if (prop == null) return NotFound();
        return Ok(prop);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PropertyDto dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
}
