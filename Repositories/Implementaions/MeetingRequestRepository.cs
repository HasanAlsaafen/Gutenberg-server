using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Models;
using Gutenburg_Server.Data;
using Gutenburg_Server.Repositories;

public class MeetingRequestRepository : IMeetingRequestRepository
{
    private readonly AppDbContext _context;

    public MeetingRequestRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MeetingRequest>> GetAllAsync()
    {
        return await _context.MeetingRequests.ToListAsync();
    }

    public async Task<MeetingRequest?> GetByIdAsync(int id)
    {
        return await _context.MeetingRequests.FindAsync(id);
    }

    public async Task<MeetingRequest> AddAsync(MeetingRequest meetingRequest)
    {
        _context.MeetingRequests.Add(meetingRequest);
        await _context.SaveChangesAsync();
        return meetingRequest;
    }

    public async Task<MeetingRequest> UpdateAsync(MeetingRequest meetingRequest)
    {
        _context.MeetingRequests.Update(meetingRequest);
        await _context.SaveChangesAsync();
        return meetingRequest;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.MeetingRequests.FindAsync(id);
        if (entity == null) return false;

        _context.MeetingRequests.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
