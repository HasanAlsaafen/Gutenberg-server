using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<Service> CreateAsync(Service service);
        Task<Service> UpdateAsync(Service service);
        Task<bool> DeleteAsync(int id);
    }
}
