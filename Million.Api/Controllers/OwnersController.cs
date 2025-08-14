using Microsoft.AspNetCore.Mvc;

namespace Million.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnersController : ControllerBase
{
    private readonly OwnerService _service;

    public OwnersController(OwnerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var owners = await _service.GetAllAsync();
        return Ok(owners);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var owner = await _service.GetByIdAsync(id);
        if (owner == null) return NotFound();
        return Ok(owner);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] OwnerDto dto)
    {
        var newOwner = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = newOwner.Id }, newOwner);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] OwnerDto dto)
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
