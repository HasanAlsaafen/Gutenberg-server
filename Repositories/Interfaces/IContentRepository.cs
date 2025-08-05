using Gutenburg_Server.Models;

namespace Gutenburg_Server.Repositories
{
    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetAllAsync();
        Task<Content?> GetByIdAsync(int id);
        Task<Content> AddAsync(Content content);
        Task<Content> UpdateAsync(Content content);
        Task<bool> DeleteAsync(int id);
    }
}
