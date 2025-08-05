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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MeetingRequest>>> GetAll()
        {
            return await _context.MeetingRequests.Include(m => m.User).ToListAsync();
        }

       


        [HttpGet("{id}")]
        public async Task<ActionResult<MeetingRequest>> GetById(int id)
        {
            var request = await _context.MeetingRequests.Include(m => m.User).FirstOrDefaultAsync(m => m.MeetingId == id);
            if (request == null) return NotFound();
            return Ok(request);
        }
        [HttpPost]
        public async Task<ActionResult> Create(MeetingRequest request)
        {
            _context.MeetingRequests.Add(request);
            await _context.SaveChangesAsync();
            return Ok(request);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var request = await _context.MeetingRequests.FindAsync(id);
            if (request == null) return NotFound();
            _context.MeetingRequests.Remove(request);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
