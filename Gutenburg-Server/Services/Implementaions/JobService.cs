using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class JobService : IJobService
    {
        private readonly IJobRepository _jobRepo;

        public JobService(IJobRepository jobRepo)
        {
            _jobRepo = jobRepo;
        }

        public async Task<IEnumerable<Job>> GetAllAsync() => await _jobRepo.GetAllAsync();

        public async Task<Job?> GetByIdAsync(int id) => await _jobRepo.GetByIdAsync(id);

        public async Task<Job> CreateAsync(Job job)
        {
            job.PostedDate = DateTime.Now;
            if (job.Deadline <= job.PostedDate)
                throw new Exception("Deadline must be in the future.");
            return await _jobRepo.AddAsync(job);
        }

        public async Task<Job> UpdateAsync(Job job)
        {
            if (job.Deadline <= job.PostedDate)
                throw new Exception("Deadline must be after posted date.");
            return await _jobRepo.UpdateAsync(job);
        }

        public async Task<bool> DeleteAsync(int id) => await _jobRepo.DeleteAsync(id);
    }
}