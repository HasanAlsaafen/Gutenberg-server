using Gutenburg_Server.DTOs;
using Gutenburg_Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Gutenburg_Server.Controllers
{
    [Authorize] 
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
        [Authorize(Roles = "Admin")]
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
        [AllowAnonymous]          public async Task<IActionResult> Create(ApplicationDTO dto)
        {
            var createdApp = await _applicationService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdApp.ApplicationId }, createdApp);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]  
        public async Task<IActionResult> Update(int id, ApplicationDTO dto)
        {
            var updatedApp = await _applicationService.UpdateAsync(id, dto);
            if (updatedApp == null) return NotFound();
            return Ok(updatedApp);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]  
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _applicationService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
