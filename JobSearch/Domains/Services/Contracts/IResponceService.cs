using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IResponceService
    {
        Task<Responce> CreateAsync(ResponceDto dto);
        Task<IEnumerable<Responce>> GetAllAsync();
        Task<Responce> GetByIdAsync(int id);
        Task UpdateAsync(int id, ResponceDto dto);
        Task DeleteAsync(int id);
    }
}
