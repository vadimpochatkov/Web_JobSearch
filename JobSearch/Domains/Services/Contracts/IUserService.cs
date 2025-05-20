using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IUserService
    {
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(int id, UserRequest dto);
        Task DeleteAsync(int id);
    }
}
