using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegistrationDto newuser);
        Task<User> LoginAsync(LoginDto dto);
        Task<User> GetUserByIdAsync(int id);
    }
}