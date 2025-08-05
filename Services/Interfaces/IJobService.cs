using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface IJobService
    {
        Task<IEnumerable<Job>> GetAllAsync();
        Task<Job?> GetByIdAsync(int id);
        Task<Job> CreateAsync(Job job);
        Task<Job> UpdateAsync(Job job);
        Task<bool> DeleteAsync(int id);
    }
}
