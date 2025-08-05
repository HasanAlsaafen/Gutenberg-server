using Microsoft.AspNetCore.Mvc;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Data;
namespace Gutenburg_Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContentController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContentController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetAll()
        {
            var contents = await _context.Contents.Include(c => c.User).ToListAsync();
            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetById(int id)
        {
            var content = await _context.Contents.Include(c => c.User).FirstOrDefaultAsync(c => c.ContentId == id);
            if (content == null) return NotFound();
            return Ok(content);
        }
        [HttpPost]
        public async Task<ActionResult> Create(Content content)
        {
            content.CreatedAt = DateTime.Now;
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
            return Ok(content);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Content updated)
        {
            if (id != updated.ContentId) return BadRequest();
            _context.Entry(updated).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null) return NotFound();
            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
