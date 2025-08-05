using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface ISolutionService
    {
        Task<IEnumerable<Solution>> GetAllAsync();
        Task<Solution?> GetByIdAsync(int id);
        Task<Solution> CreateAsync(Solution solution);
        Task<Solution> UpdateAsync(Solution solution);
        Task<bool> DeleteAsync(int id);
    }
}
