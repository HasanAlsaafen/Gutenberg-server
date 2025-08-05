using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repo;

        public ServiceService(IServiceRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Service>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Service?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Service> CreateAsync(Service service)
            => await _repo.AddAsync(service);

        public async Task<Service> UpdateAsync(Service service)
            => await _repo.UpdateAsync(service);

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
