using Gutenburg_Server.Models;

namespace Gutenburg_Server.Repositories
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<Service> AddAsync(Service service);
        Task<Service> UpdateAsync(Service service);
        Task<bool> DeleteAsync(int id);
    }
}
