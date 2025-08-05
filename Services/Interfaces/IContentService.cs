using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface IContentService
    {
        Task<IEnumerable<Content>> GetAllAsync();
        Task<Content?> GetByIdAsync(int id);
        Task<Content> CreateAsync(Content content);
        Task<Content> UpdateAsync(Content content);
        Task<bool> DeleteAsync(int id);
    }
}