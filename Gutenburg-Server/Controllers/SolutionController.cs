// Sol
using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SolutionController : ControllerBase
{
    private readonly ISolutionService _solutionService;

    public SolutionController(ISolutionService solutionService)
    {
        _solutionService = solutionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var solutions = await _solutionService.GetAllAsync();
        var dtos = solutions.Select(s => new SolutionDTO
        {
            SolutionId = s.SolutionId,
            Title = s.Title,
            Description = s.Description,
            SolutionType = s.SolutionType.ToString(),
            Image = s.Image
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var solution = await _solutionService.GetByIdAsync(id);
        if (solution == null) return NotFound();

        var dto = new SolutionDTO
        {
            SolutionId = solution.SolutionId,
            Title = solution.Title,
            Description = solution.Description,
            SolutionType = solution.SolutionType.ToString(),
            Image = solution.Image
        };
        return Ok(dto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SolutionDTO dto)
    {
        var solution = new Solution
        {
            Title = dto.Title,
            Description = dto.Description,
            SolutionType = Enum.Parse<SolutionType>(dto.SolutionType),
            Image = dto.Image
        };

        var created = await _solutionService.CreateAsync(solution);
        dto.SolutionId = created.SolutionId;
        return CreatedAtAction(nameof(GetById), new { id = dto.SolutionId }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] SolutionDTO dto)
    {
        var existing = await _solutionService.GetByIdAsync(id);
        if (existing == null) return NotFound();

        existing.Title = dto.Title;
        existing.Description = dto.Description;
        existing.SolutionType = Enum.Parse<SolutionType>(dto.SolutionType);
        existing.Image = dto.Image;

        var updated = await _solutionService.UpdateAsync(existing);
        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _solutionService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
