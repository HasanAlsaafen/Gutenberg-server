using Gutenburg_Server.DTOs;

namespace Gutenburg_Server.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<ApplicationDTO>> GetAllAsync();
        Task<ApplicationDTO?> GetByIdAsync(int id);
        Task<ApplicationDTO> CreateAsync(ApplicationDTO dto);
        Task<ApplicationDTO?> UpdateAsync(int id, ApplicationDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
