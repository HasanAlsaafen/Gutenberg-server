using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repo;

        public MessageService(IMessageRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Message>> GetAllAsync() => await _repo.GetAllAsync();

        public async Task<Message?> GetByIdAsync(int id) => await _repo.GetByIdAsync(id);

        public async Task<Message> CreateAsync(Message message)
        {
            message.DateSent = DateTime.Now;
            return await _repo.AddAsync(message);
        }

        public async Task<Message> UpdateAsync(Message message)
            => await _repo.UpdateAsync(message);

        public async Task<bool> DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
