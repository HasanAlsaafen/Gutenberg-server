using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface IMeetingRequestService
    {
        Task<IEnumerable<MeetingRequest>> GetAllAsync();
        Task<MeetingRequest?> GetByIdAsync(int id);
        Task<MeetingRequest> CreateAsync(MeetingRequest meeting);
        Task<MeetingRequest> UpdateAsync(MeetingRequest meeting);
        Task<bool> DeleteAsync(int id);
    }
}
