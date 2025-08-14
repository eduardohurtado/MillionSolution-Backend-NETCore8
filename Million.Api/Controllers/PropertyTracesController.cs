using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PropertyTracesController : ControllerBase
{
    private readonly PropertyTraceService _service;

    public PropertyTracesController(PropertyTraceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ptraces = await _service.GetAllAsync();
        return Ok(ptraces);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var ptrace = await _service.GetByIdAsync(id);
        if (ptrace == null) return NotFound();
        return Ok(ptrace);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PropertyTraceDto dto)
    {
        var newPropertyTrace = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] PropertyTraceDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
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
