using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PropertyImagesController : ControllerBase
{
    private readonly PropertyImageService _service;

    public PropertyImagesController(PropertyImageService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var images = await _service.GetAllAsync();
        return Ok(images);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var image = await _service.GetByIdAsync(id);
        if (image == null) return NotFound();
        return Ok(image);
    }

    [HttpPost]
    public async Task<IActionResult> Create(PropertyImageDto dto)
    {
        var entity = new PropertyImage
        {
            IdProperty = dto.IdProperty,
            File = dto.File,
            Enabled = dto.Enabled
        };

        await _service.AddAsync(entity);
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, PropertyImageDto dto)
    {
        var entity = new PropertyImage
        {
            Id = id,
            IdProperty = dto.IdProperty,
            File = dto.File,
            Enabled = dto.Enabled
        };

        var updated = await _service.UpdateAsync(id, entity);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
