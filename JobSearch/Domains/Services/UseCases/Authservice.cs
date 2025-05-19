using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;
using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.UseCases
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _repository;

        public AuthService(IUserRepository repository) => _repository = repository;

        public async Task<User> RegisterAsync(RegistrationDto newuser)
        {
            var existingUser = await _repository.GetUserByEmailAsync(newuser.Email);
            if (existingUser != null) throw new InvalidOperationException("User already exists");
            return await _repository.CreateUserAsync(newuser);
        }

        public async Task<User> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            return user?.Password == dto.Password ? user : null;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }
    }
}