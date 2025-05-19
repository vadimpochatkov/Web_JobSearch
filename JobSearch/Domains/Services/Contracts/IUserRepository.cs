using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IUserRepository
    {
        // User methods
        Task<User> CreateUserAsync(RegistrationDto newuser);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);
    }
}