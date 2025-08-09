using Gutenburg_Server.DTOs;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetAll()
        {
            var apps = await _applicationService.GetAllAsync();
            return Ok(apps);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var app = await _applicationService.GetByIdAsync(id);
            if (app == null) return NotFound();
            return Ok(app);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ApplicationDTO dto)
        {
            var createdApp = await _applicationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdApp.ApplicationId }, createdApp);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ApplicationDTO dto)
        {
            var updatedApp = await _applicationService.UpdateAsync(id, dto);
            if (updatedApp == null) return NotFound();
            return Ok(updatedApp);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _applicationService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
