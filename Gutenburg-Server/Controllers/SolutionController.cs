using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Gutenburg_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SolutionController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Solution>>> GetAll()
        {
            return await _context.Solutions.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Solution solution)
        {
            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();
            return Ok(solution);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null) return NotFound();
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
