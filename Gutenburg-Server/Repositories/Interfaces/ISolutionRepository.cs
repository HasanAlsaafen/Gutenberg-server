using Gutenburg_Server.Models;

namespace Gutenburg_Server.Repositories
{
    public interface ISolutionRepository
    {
        Task<IEnumerable<Solution>> GetAllAsync();
        Task<Solution?> GetByIdAsync(int id);
        Task<Solution> AddAsync(Solution solution);
        Task<Solution> UpdateAsync(Solution solution);
        Task<bool> DeleteAsync(int id);
    }
}
