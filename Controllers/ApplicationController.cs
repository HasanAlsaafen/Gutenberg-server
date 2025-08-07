using Microsoft.AspNetCore.Mvc;
using Gutenburg_Server.Models;
using Gutenburg_Server.DTOs;
using Gutenburg_Server.Services;

namespace Gutenburg_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationDTO>>> GetAll()
        {
            var applications = await _applicationService.GetAllAsync();
            var dtos = applications.Select(app => new ApplicationDTO
            {
                ApplicationId = app.ApplicationId,
                JobId = app.JobId,
                UserId = app.UserId,
                Attachment = app.Attachment,
                ApplicationDate = app.ApplicationDate,
                AppStatus = (ApplicationStatusDTO)Enum.Parse(typeof(ApplicationStatusDTO), app.ApplicationStatus.ToString())
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationDTO>> GetById(int id)
        {
            var app = await _applicationService.GetByIdAsync(id);
            if (app == null) return NotFound();

            var dto = new ApplicationDTO
            {
                ApplicationId = app.ApplicationId,
                JobId = app.JobId,
                UserId = app.UserId,
                Attachment = app.Attachment,
                ApplicationDate = app.ApplicationDate,
                AppStatus = (ApplicationStatusDTO)Enum.Parse(typeof(ApplicationStatusDTO), app.ApplicationStatus.ToString())
            };
            return Ok(dto);
        }

       [HttpPost]
public async Task<ActionResult<ApplicationDTO>> Create([FromBody] ApplicationDTO dto)
{
    try
    {
        var app = new Application
        {
            JobId = dto.JobId,
            UserId = dto.UserId,
            Attachment = dto.Attachment
        };

        var created = await _applicationService.CreateAsync(app);

        dto.ApplicationId = created.ApplicationId;
        dto.ApplicationDate = created.ApplicationDate;
        dto.AppStatus = (ApplicationStatusDTO)Enum.Parse(typeof(ApplicationStatusDTO), created.ApplicationStatus.ToString());

        return CreatedAtAction(nameof(GetById), new { id = dto.ApplicationId }, dto);
    }
 catch (Exception ex)
{
    var message = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
    return StatusCode(500, $"Internal server error: {message}");
}
}

        [HttpPut("{id}")]
        public async Task<ActionResult<ApplicationDTO>> Update(int id, [FromBody] ApplicationDTO dto)
        {
            var existing = await _applicationService.GetByIdAsync(id);
            if (existing == null) return NotFound();

            existing.JobId = dto.JobId;
            existing.UserId = dto.UserId;
            existing.Attachment = dto.Attachment;
            existing.ApplicationStatus = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), dto.AppStatus.ToString());

            var updated = await _applicationService.UpdateAsync(existing);

            dto.ApplicationDate = updated.ApplicationDate;
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _applicationService.DeleteAsync(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}