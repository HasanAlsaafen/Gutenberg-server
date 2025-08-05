using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _repo;

        public ContentService(IContentRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Content>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Content?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Content> CreateAsync(Content content)
        {
            content.CreatedAt = DateTime.Now;
            return await _repo.AddAsync(content);
        }

        public async Task<Content> UpdateAsync(Content content)
            => await _repo.UpdateAsync(content);

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}