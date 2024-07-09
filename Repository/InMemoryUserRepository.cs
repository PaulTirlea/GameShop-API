using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SummerPracticePaul.Models;
using SummerPracticePaul.Repository.Data;
using SummerPracticePaul.Repository.Interface;

namespace SummerPracticePaul.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        private readonly UserManager<User> _userManager;

        public InMemoryUserRepository(UserDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users;

        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(r => r.Id == userId);
        }

    }
}
