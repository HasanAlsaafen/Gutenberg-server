using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Repositories
{
    public class MeetingRequestRepository : IMeetingRequestRepository
    {
        private readonly AppDbContext _context;
        public MeetingRequestRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<MeetingRequest>> GetAllAsync()
            => await _context.MeetingRequests.Include(r => r.User).ToListAsync();

        public async Task<MeetingRequest?> GetByIdAsync(int id)
            => await _context.MeetingRequests.Include(r => r.User).FirstOrDefaultAsync(r => r.MeetingId == id);

        public async Task<MeetingRequest> AddAsync(MeetingRequest request)
        {
            _context.MeetingRequests.Add(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<MeetingRequest> UpdateAsync(MeetingRequest request)
        {
            _context.MeetingRequests.Update(request);
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var req = await _context.MeetingRequests.FindAsync(id);
            if (req == null) return false;
            _context.MeetingRequests.Remove(req);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
