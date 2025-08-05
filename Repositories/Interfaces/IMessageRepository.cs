using Gutenburg_Server.Models;

public interface IMessageRepository
{
    Task<IEnumerable<Message>> GetAllAsync();
    Task<Message?> GetByIdAsync(int id);
    Task<Message> AddAsync(Message message);
    Task<Message> UpdateAsync(Message message);
    Task<bool> DeleteAsync(int id);
}
