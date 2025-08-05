// ApplicationController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Gutenburg_Server.DTOs;

namespace Gutenburg_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ApplicationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<ApplicationDTO>> GetApplications()
        {
            var applications = await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Job)
                .ToListAsync();

            var dtos = applications.Select(a => new ApplicationDTO
            {
                ApplicationId = a.ApplicationId,
                JobId = a.JobId,
                UserId = a.UserId,
                Attachment = a.Attachment,
                ApplicationDate = a.ApplicationDate,
                AppStatus = (Gutenburg_Server.DTOs.ApplicationStatusDTO)a.ApplicationStatus 
            });

            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Application application)
        {
            application.ApplicationDate = DateTime.Now;
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return Ok(application);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Application updated)
        {
            if (id != updated.ApplicationId)
                return BadRequest();

            var existing = await _context.Applications.FindAsync(id);
            if (existing == null)
                return NotFound();

            existing.Attachment = updated.Attachment;
            existing.ApplicationStatus = updated.ApplicationStatus;
            existing.JobId = updated.JobId;
            existing.UserId = updated.UserId;

            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _context.Applications.FindAsync(id);
            if (application == null) return NotFound();

            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
