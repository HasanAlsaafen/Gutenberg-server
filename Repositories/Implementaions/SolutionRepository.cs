using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Repositories
{
    public class SolutionRepository : ISolutionRepository
    {
        private readonly AppDbContext _context;
        public SolutionRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Solution>> GetAllAsync()
            => await _context.Solutions.ToListAsync();

        public async Task<Solution?> GetByIdAsync(int id)
            => await _context.Solutions.FindAsync(id);

        public async Task<Solution> AddAsync(Solution solution)
        {
            _context.Solutions.Add(solution);
            await _context.SaveChangesAsync();
            return solution;
        }

        public async Task<Solution> UpdateAsync(Solution solution)
        {
            _context.Solutions.Update(solution);
            await _context.SaveChangesAsync();
            return solution;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var solution = await _context.Solutions.FindAsync(id);
            if (solution == null) return false;
            _context.Solutions.Remove(solution);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
