using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IResponseService
    {
        Task<Responce> CreateAsync(ResponseDto dto);
        Task<IEnumerable<Responce>> GetAllAsync();
        Task<Responce> GetByIdAsync(int id);
        Task UpdateAsync(int id, ResponseDto dto);
        Task DeleteAsync(int id);
    }
}
