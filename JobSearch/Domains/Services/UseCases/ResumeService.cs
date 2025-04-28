using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.UseCases;
public class ResumeService : IResumeService
{
    private readonly IRepository _repository;

    public ResumeService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<Resume> CreateResumeAsync(ResumeDto dto)
    {
        var resume = new Resume
        {
            UserId = dto.UserId,
            Title = dto.Title,
            Education = dto.Education,
            Experience = dto.Experience
        };
        return await _repository.CreateResumeAsync(resume);
    }

    public async Task DeleteResumeAsync(int id)
    {
        await _repository.DeleteResumeAsync(id);
    }

    public async Task<IEnumerable<Resume>> GetAllResumesAsync()
    {
        return await _repository.GetAllResumesAsync();
    }

    public async Task<Resume> GetResumeByIdAsync(int id)
    {
        return await _repository.GetResumeByIdAsync(id);
    }

    public async Task UpdateResumeAsync(int id, ResumeDto dto)
    {
        var resume = await _repository.GetResumeByIdAsync(id);
        resume.Title = dto.Title;
        resume.Education = dto.Education;
        resume.Experience = dto.Experience;
        await _repository.UpdateResumeAsync(resume);
    }
}