using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application?> GetByIdAsync(int id);
        Task<Application> CreateAsync(Application application);
        Task<Application> UpdateAsync(Application application);
        Task<bool> DeleteAsync(int id);
    }
}
