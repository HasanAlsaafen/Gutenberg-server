using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;
using Gutenburg_Server.Services; 

namespace Gutenburg_Server.Services
{
    public class MeetingRequestService : IMeetingRequestService
    {
        private readonly IMeetingRequestRepository _repo;

        public MeetingRequestService(IMeetingRequestRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<MeetingRequest>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<MeetingRequest?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<MeetingRequest> CreateAsync(MeetingRequest meeting)
        {
            meeting.MeetingStatus = MeetingStatus.Pending;
            return await _repo.AddAsync(meeting);
        }

        public async Task<MeetingRequest> UpdateAsync(MeetingRequest meeting)
            => await _repo.UpdateAsync(meeting);

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
