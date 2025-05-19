using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IResumeRepository
    {
        Task<Resume> CreateResumeAsync(Resume resume);
        Task<Resume> GetResumeByIdAsync(int id);
        Task UpdateResumeAsync(Resume resume);
        Task DeleteResumeAsync(int id);
        Task<List<Resume>> GetResumesByUserAsync(int userId);
        Task<IEnumerable<Resume>> GetAllResumesAsync();
    }
}