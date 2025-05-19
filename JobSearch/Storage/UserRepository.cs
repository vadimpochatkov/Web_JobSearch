using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Storage
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // User methods
        public async Task<User> CreateUserAsync(RegistrationDto newUserDto)
        {
            var user = newUserDto.ToUser();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.UserId} not found");
            }

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}