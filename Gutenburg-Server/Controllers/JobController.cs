using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Gutenburg_Server.DTOs;

namespace Gutenburg_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<JobDTO>> GetJobs()
        {
            var jobs = await _context.Jobs.Include(j => j.User).ToListAsync();
            var dtos = jobs.Select(j => new JobDTO
            {
                JobId = j.JobId,
                Title = j.Title,
                Description = j.Description,
                PostedDate = j.PostedDate,
                Deadline = j.Deadline,
                PostedBy = j.User.Name
            });

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Job job)
        {
            job.PostedDate = DateTime.Now;
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();
            return Ok(job);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null) return NotFound();
            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}