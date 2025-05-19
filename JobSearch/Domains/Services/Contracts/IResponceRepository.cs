using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IResponceRepository
    {
        Task<Responce> CreateResponceAsync(Responce application);
        Task<Responce> GetResponceByIdAsync(int id);
        Task UpdateResponceAsync(Responce application);
        Task DeleteResponceAsync(int id);
        Task<List<Responce>> GetResponcesByUserAsync(int userId);
        Task<IEnumerable<Responce>> GetAllResponcesAsync();
    }
}