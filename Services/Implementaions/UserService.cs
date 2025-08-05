using Gutenburg_Server.Models;
using Gutenburg_Server.Repositories;

namespace Gutenburg_Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepo.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id) => await _userRepo.GetByIdAsync(id);

        public async Task<User> CreateAsync(User user)
        {
            var existing = (await _userRepo.GetAllAsync()).FirstOrDefault(u => u.Email == user.Email);
            if (existing != null)
                throw new Exception("Email is already taken.");

            user.Password = user.Password; // TODO: hash the password
            return await _userRepo.AddAsync(user);
        }

        public async Task<User> UpdateAsync(User user) => await _userRepo.UpdateAsync(user);

        public async Task<bool> DeleteAsync(int id) => await _userRepo.DeleteAsync(id);
    }
}
