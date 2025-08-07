using Gutenburg_Server.Repositories;
using Gutenburg_Server.Models;
namespace Gutenburg_Server.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _appRepo;
        private readonly IJobRepository _jobRepo;

        public ApplicationService(IApplicationRepository appRepo, IJobRepository jobRepo)
        {
            _appRepo = appRepo;
            _jobRepo = jobRepo;
        }

        public async Task<IEnumerable<Application>> GetAllAsync() => await _appRepo.GetAllAsync();

        public async Task<Application?> GetByIdAsync(int id) => await _appRepo.GetByIdAsync(id);

        public async Task<Application> CreateAsync(Application application)
        {
            var job = await _jobRepo.GetByIdAsync(application.JobId);
            if (job == null || job.Deadline < DateTime.Now)
                throw new Exception("Can't apply to a closed or non-existent job.");

            application.ApplicationDate = DateTime.UtcNow;
            application.ApplicationStatus = ApplicationStatus.Pending;
            return await _appRepo.AddAsync(application);
        }

        public async Task<Application> UpdateAsync(Application application)
            => await _appRepo.UpdateAsync(application);

        public async Task<bool> DeleteAsync(int id) => await _appRepo.DeleteAsync(id);
        
    }
}
