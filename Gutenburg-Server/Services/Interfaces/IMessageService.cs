using Gutenburg_Server.Models;

namespace Gutenburg_Server.Services
{
    public interface IMessageService
    {
        Task<IEnumerable<Message>> GetAllAsync();
        Task<Message?> GetByIdAsync(int id);
        Task<Message> CreateAsync(Message message);
        Task<Message> UpdateAsync(Message message);
        Task<bool> DeleteAsync(int id);
    }
}