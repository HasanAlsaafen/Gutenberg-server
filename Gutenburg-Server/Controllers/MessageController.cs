using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MessageController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAll()
        {
            return await _context.Messages.Include(m => m.User).ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetById(int id)
        {
            var message = await _context.Messages.Include(m => m.User).FirstOrDefaultAsync(m => m.MessageId == id);
            if (message == null) return NotFound();
            return Ok(message);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Message message)
        {
            message.DateSent = DateTime.Now;
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null) return NotFound();
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
