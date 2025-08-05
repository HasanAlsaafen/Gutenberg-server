using Gutenburg_Server.Models;

public interface IJobRepository
{
    Task<IEnumerable<Job>> GetAllAsync();
    Task<Job?> GetByIdAsync(int id);
    Task<Job> AddAsync(Job job);
    Task<Job> UpdateAsync(Job job);
    Task<bool> DeleteAsync(int id);
}
