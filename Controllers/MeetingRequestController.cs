using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Gutenburg_Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class MeetingRequestController : ControllerBase
{
    private readonly IMeetingRequestService _service;

    public MeetingRequestController(IMeetingRequestService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize]  
    public async Task<ActionResult<IEnumerable<MeetingRequestDTO>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    [Authorize]  
    public async Task<ActionResult<MeetingRequestDTO>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    [AllowAnonymous]  
    public async Task<ActionResult<MeetingRequestDTO>> Create(MeetingRequestDTO dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.MeetingId }, created);
    }

    [HttpPut("{id}")]
    [Authorize]  
    public async Task<ActionResult<MeetingRequestDTO>> Update(int id, MeetingRequestDTO dto)
    {
        if (id != dto.MeetingId)
            return BadRequest();

        var updated = await _service.UpdateAsync(dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    [Authorize]  
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
