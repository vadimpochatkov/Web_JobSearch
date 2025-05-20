using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.UseCases
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task UpdateAsync(int id, UserRequest dto)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) throw new Exception("User not found");

            user.Name = dto.Name;
            user.Phone = dto.Phone;
            user.Email = dto.Email;

            await _repository.UpdateUserAsync(user);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteUserAsync(id);
        }
    }
}
