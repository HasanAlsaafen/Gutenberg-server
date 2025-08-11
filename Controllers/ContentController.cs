using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "Admin,User")]
[ApiController]
[Route("api/[controller]")]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var contents = await _contentService.GetAllAsync();
        var dtos = contents.Select(c => new ContentDTO
        {
            ContentId = c.ContentId,
            UserId = c.UserId,
            Title = c.Title,
            Body = c.Body,
            PageType = c.PageType.ToString(),
            CreatedAt = c.CreatedAt
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var content = await _contentService.GetByIdAsync(id);
        if (content == null) return NotFound();

        var dto = new ContentDTO
        {
            ContentId = content.ContentId,
            UserId = content.UserId,
            Title = content.Title,
            Body = content.Body,
            PageType = content.PageType.ToString(),
            CreatedAt = content.CreatedAt
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ContentDTO dto)
    {
        var content = new Content
        {
            UserId = dto.UserId,
            Title = dto.Title,
            Body = dto.Body,
            PageType = Enum.Parse<PageType>(dto.PageType),
            CreatedAt = DateTime.Now
        };

        var created = await _contentService.CreateAsync(content);
        dto.ContentId = created.ContentId;
        dto.CreatedAt = created.CreatedAt;
        return CreatedAtAction(nameof(GetById), new { id = dto.ContentId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ContentDTO dto)
    {
        var existing = await _contentService.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Title = dto.Title;
        existing.Body = dto.Body;
        existing.PageType = Enum.Parse<PageType>(dto.PageType);

        var updated = await _contentService.UpdateAsync(existing);
        dto.CreatedAt = updated.CreatedAt;
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _contentService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
