using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts;
public interface IResumeService
{
    Task<Resume> CreateResumeAsync(ResumeRequest dto);
    Task<IEnumerable<Resume>> GetAllResumesAsync();
    Task<Resume> GetResumeByIdAsync(int id);
    Task UpdateResumeAsync(int id, ResumeRequest dto);
    Task DeleteResumeAsync(int id);
}