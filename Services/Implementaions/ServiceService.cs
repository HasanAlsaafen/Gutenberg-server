using Gutenburg_Server.DTOs;
using Gutenburg_Server.Models;
using Microsoft.EntityFrameworkCore;
using Gutenburg_Server.Data;
namespace Gutenburg_Server.Services
{
    public class ServiceService : IServiceService
    {
        private readonly AppDbContext _context;

        public ServiceService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task<Service> CreateAsync(Service service)
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
            var entity = await _context.Services.FindAsync(id);
            if (entity == null)
                return false;

            _context.Services.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task CreateAsync(ServiceDTO dto)
        {
            var service = new Service
            {
                Title = dto.Title,
                Description = dto.Description,
                Image =dto.Image

            };

            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }
    }
}
