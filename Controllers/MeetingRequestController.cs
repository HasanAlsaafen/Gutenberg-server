using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gutenburg_Server.DTOs;

[ApiController]
[Route("api/[controller]")]
public class MeetingRequestsController : ControllerBase
{
    private readonly IMeetingRequestService _service;

    public MeetingRequestsController(IMeetingRequestService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MeetingRequestDTO>>> GetAll()
    {
        var list = await _service.GetAllAsync();
        return Ok(list);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MeetingRequestDTO>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<MeetingRequestDTO>> Create(MeetingRequestDTO dto)
    {
        var created = await _service.AddAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.MeetingId }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MeetingRequestDTO>> Update(int id, MeetingRequestDTO dto)
    {
        if (id != dto.MeetingId)
            return BadRequest();

        var updated = await _service.UpdateAsync(dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
