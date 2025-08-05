using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Repositories
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;
        public ContentRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Content>> GetAllAsync()
            => await _context.Contents.Include(c => c.User).ToListAsync();

        public async Task<Content?> GetByIdAsync(int id)
            => await _context.Contents.Include(c => c.User).FirstOrDefaultAsync(c => c.ContentId == id);

        public async Task<Content> AddAsync(Content content)
        {
            _context.Contents.Add(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task<Content> UpdateAsync(Content content)
        {
            _context.Contents.Update(content);
            await _context.SaveChangesAsync();
            return content;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var content = await _context.Contents.FindAsync(id);
            if (content == null) return false;
            _context.Contents.Remove(content);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
