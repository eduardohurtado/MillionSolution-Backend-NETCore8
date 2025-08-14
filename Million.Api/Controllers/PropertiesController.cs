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

    // GET: api/properties
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PropertyDto>>> GetAll()
    {
        var properties = await _service.GetAllAsync();
        return Ok(properties);
    }

    // GET: api/properties/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<PropertyDto>> GetById(string id)
    {
        var property = await _service.GetByIdAsync(id);
        if (property == null)
            return NotFound(new { message = $"Property with id '{id}' not found." });

        return Ok(property);
    }

    // POST: api/properties
    [HttpPost]
    public async Task<ActionResult<PropertyDto>> Create([FromBody] PropertyDto propertyDto)
    {
        if (propertyDto == null)
            return BadRequest(new { message = "Invalid property data." });

        var newProperty = await _service.AddAsync(propertyDto);

        // Ensure Id is set after insert
        return CreatedAtAction(
            nameof(GetById),
            new { id = newProperty.Id },
            newProperty
        );
    }

    // PUT: api/properties/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] PropertyDto propertyDto)
    {
        if (propertyDto == null)
            return BadRequest(new { message = "Invalid property data." });

        var updated = await _service.UpdateAsync(id, propertyDto);

        if (!updated)
            return NotFound(new { message = $"Property with id '{id}' not found." });

        return NoContent(); // 204
    }

    // DELETE: api/properties/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
            return NotFound(new { message = $"Property with id '{id}' not found." });

        return NoContent();
    }
}
