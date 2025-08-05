using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _repo;

        public SolutionService(ISolutionRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Solution>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Solution?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Solution> CreateAsync(Solution solution)
            => await _repo.AddAsync(solution);

        public async Task<Solution> UpdateAsync(Solution solution)
            => await _repo.UpdateAsync(solution);

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
