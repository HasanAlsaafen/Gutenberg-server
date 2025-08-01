using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class MeetingRequestController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MeetingRequestController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMeetingRequest([FromBody] MeetingRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userExists = await _context.Users.AnyAsync(u => u.UserId == request.UserId);
            if (!userExists)
                return BadRequest(new { error = "Invalid user ID." });

            if (request.PreferredDate == DateTime.MinValue)
                return BadRequest(new { error = "Preferred date is required." });

            if (request.PreferredDate < DateTime.Now)
                return BadRequest(new { error = "Preferred date must be in the future." });

            request.MeetingStatus = MeetingStatus.Pending;
            request.ResponseDate = null;


            _context.MeetingRequests.Add(request);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Meeting request submitted successfully." });
        }
    }
}
