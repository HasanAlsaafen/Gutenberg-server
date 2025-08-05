using Gutenburg_Server.Models;

namespace Gutenburg_Server.Repositories
{
    public interface IMeetingRequestRepository
    {
        Task<IEnumerable<MeetingRequest>> GetAllAsync();
        Task<MeetingRequest?> GetByIdAsync(int id);
        Task<MeetingRequest> AddAsync(MeetingRequest request);
        Task<MeetingRequest> UpdateAsync(MeetingRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
