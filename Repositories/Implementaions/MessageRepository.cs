using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;
    public MessageRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<Message>> GetAllAsync()
        => await _context.Messages.Include(m => m.User).ToListAsync();

    public async Task<Message?> GetByIdAsync(int id)
        => await _context.Messages.Include(m => m.User).FirstOrDefaultAsync(m => m.MessageId == id);

    public async Task<Message> AddAsync(Message message)
    {
        _context.Messages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<Message> UpdateAsync(Message message)
    {
        _context.Messages.Update(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var message = await _context.Messages.FindAsync(id);
        if (message == null) return false;
        _context.Messages.Remove(message);
        await _context.SaveChangesAsync();
        return true;
    }
}
