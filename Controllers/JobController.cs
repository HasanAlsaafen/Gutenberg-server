using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


[Authorize]  
[ApiController]
[Route("api/[controller]")]
public class JobController : ControllerBase
{
    private readonly IJobService _jobService;

    public JobController(IJobService jobService)
    {
        _jobService = jobService;
    }

    [HttpGet]
    [AllowAnonymous]  


    public async Task<IActionResult> GetAll()
    {
        var jobs = await _jobService.GetAllAsync();
        var dtos = jobs.Select(j => new JobDTO
        {
            JobId = j.JobId,
            Title = j.Title,
            Description = j.Description,
            PostedDate = j.PostedDate,
            Deadline = j.Deadline,
        });
        return Ok(dtos);
    }

    [HttpGet("{id}")]


    public async Task<IActionResult> GetById(int id)
    {
        var job = await _jobService.GetByIdAsync(id);
        if (job == null) return NotFound();

        var dto = new JobDTO
        {
            JobId = job.JobId,
            Title = job.Title,
            Description = job.Description,
            PostedDate = job.PostedDate,
            Deadline = job.Deadline,
        };
        return Ok(dto);
    }

    [HttpPost]
        [Authorize(Roles = "Admin")]  

    public async Task<IActionResult> Create([FromBody] JobDTO dto)
    {
        var job = new Job
        {
            Title = dto.Title,
            Description = dto.Description,
            PostedDate = dto.PostedDate == default ? DateTime.UtcNow : dto.PostedDate,
            Deadline = dto.Deadline,
            UserId = dto.UserId
        };

        var createdJob = await _jobService.CreateAsync(job);
        dto.JobId = createdJob.JobId;

        return CreatedAtAction(nameof(GetById), new { id = dto.JobId }, dto);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]  
    public async Task<IActionResult> Update(int id, [FromBody] JobDTO dto)
    {
        if (id != dto.JobId) return BadRequest();

        var job = await _jobService.GetByIdAsync(id);
        if (job == null) return NotFound();

        job.Title = dto.Title;
        job.Description = dto.Description;
        job.PostedDate = dto.PostedDate;
        job.Deadline = dto.Deadline;

        await _jobService.UpdateAsync(job);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]  
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _jobService.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
