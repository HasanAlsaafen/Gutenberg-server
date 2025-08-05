using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MeetingRequestController : ControllerBase
{
    private readonly IMeetingRequestService _meetingService;

    public MeetingRequestController(IMeetingRequestService meetingService)
    {
        _meetingService = meetingService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var meetings = await _meetingService.GetAllAsync();
        var dtos = meetings.Select(m => new MeetingRequestDTO
        {
            MeetingId = m.MeetingId,
            UserId = m.UserId,
            Topic = m.Topic,
            PreferredDate = m.PreferredDate,
            MeetingStatus = m.MeetingStatus.ToString(),
            ResponseDate = m.ResponseDate
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var meeting = await _meetingService.GetByIdAsync(id);
        if (meeting == null) return NotFound();

        var dto = new MeetingRequestDTO
        {
            MeetingId = meeting.MeetingId,
            UserId = meeting.UserId,
            Topic = meeting.Topic,
            PreferredDate = meeting.PreferredDate,
            MeetingStatus = meeting.MeetingStatus.ToString(),
            ResponseDate = meeting.ResponseDate
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MeetingRequestDTO dto)
    {
        var meeting = new MeetingRequest
        {
            UserId = dto.UserId,
            Topic = dto.Topic,
            PreferredDate = dto.PreferredDate,
            MeetingStatus = MeetingStatus.Pending
        };

        var created = await _meetingService.CreateAsync(meeting);
        dto.MeetingId = created.MeetingId;
        dto.MeetingStatus = created.MeetingStatus.ToString();
        dto.ResponseDate = created.ResponseDate;
        return CreatedAtAction(nameof(GetById), new { id = dto.MeetingId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MeetingRequestDTO dto)
    {
        var existing = await _meetingService.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Topic = dto.Topic;
        existing.PreferredDate = dto.PreferredDate;
        if (Enum.TryParse(dto.MeetingStatus, out MeetingStatus status))
        {
            existing.MeetingStatus = status;
        }
        existing.ResponseDate = dto.ResponseDate;

        var updated = await _meetingService.UpdateAsync(existing);
        dto.MeetingStatus = updated.MeetingStatus.ToString();
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _meetingService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
