
using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var messages = await _messageService.GetAllAsync();
        var dtos = messages.Select(m => new MessageDTO
        {
            MessageId = m.MessageId,
            UserId = m.UserId,
            Subject = m.Subject,
            MessageContent = m.MessageContent,
            DateSent = m.DateSent
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var msg = await _messageService.GetByIdAsync(id);
        if (msg == null) return NotFound();

        var dto = new MessageDTO
        {
            MessageId = msg.MessageId,
            UserId = msg.UserId,
            Subject = msg.Subject,
            MessageContent = msg.MessageContent,
            DateSent = msg.DateSent
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MessageDTO dto)
    {
        var msg = new Message
        {
            UserId = dto.UserId,
            Subject = dto.Subject,
            MessageContent = dto.MessageContent,
            DateSent = DateTime.Now
        };

        var created = await _messageService.CreateAsync(msg);
        dto.MessageId = created.MessageId;
        dto.DateSent = created.DateSent;
        return CreatedAtAction(nameof(GetById), new { id = dto.MessageId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] MessageDTO dto)
    {
        var existing = await _messageService.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Subject = dto.Subject;
        existing.MessageContent = dto.MessageContent;

        var updated = await _messageService.UpdateAsync(existing);
        dto.DateSent = updated.DateSent;
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _messageService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
