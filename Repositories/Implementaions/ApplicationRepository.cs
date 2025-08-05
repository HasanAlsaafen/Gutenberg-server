using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly AppDbContext _context;

        public ApplicationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Job)
                .ToListAsync();
        }

        public async Task<Application?> GetByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.User)
                .Include(a => a.Job)
                .FirstOrDefaultAsync(a => a.ApplicationId == id);
        }

        public async Task<Application> AddAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<Application> UpdateAsync(Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Applications.FindAsync(id);
            if (existing == null)
                return false;

            _context.Applications.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
