using Gutenburg_Server.Data;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Models;

public class JobRepository : IJobRepository
{
    private readonly AppDbContext _context;
    public JobRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Job>> GetAllAsync()
        => await _context.Jobs.Include(j => j.User).ToListAsync();

    public async Task<Job?> GetByIdAsync(int id)
        => await _context.Jobs.Include(j => j.User).FirstOrDefaultAsync(j => j.JobId == id);

    public async Task<Job> AddAsync(Job job)
    {
        _context.Jobs.Add(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<Job> UpdateAsync(Job job)
    {
        _context.Jobs.Update(job);
        await _context.SaveChangesAsync();
        return job;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var job = await _context.Jobs.FindAsync(id);
        if (job == null) return false;
        _context.Jobs.Remove(job);
        await _context.SaveChangesAsync();
        return true;
    }
}
