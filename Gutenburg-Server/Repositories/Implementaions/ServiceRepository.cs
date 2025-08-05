using Gutenburg_Server.Data;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Gutenburg_Server.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly AppDbContext _context;
        public ServiceRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Service>> GetAllAsync()
            => await _context.Services.ToListAsync();

        public async Task<Service?> GetByIdAsync(int id)
            => await _context.Services.FindAsync(id);

        public async Task<Service> AddAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<Service> UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
            return service;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var service = await _context.Services.FindAsync(id);
            if (service == null) return false;
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
