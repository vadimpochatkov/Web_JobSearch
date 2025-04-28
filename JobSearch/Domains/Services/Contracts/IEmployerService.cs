using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IEmployerService
    {
        Task<Employer> CreateAsync(EmployerDto dto);
        Task<Employer> GetByIdAsync(int id);
        Task UpdateAsync(int id, EmployerDto dto);
        Task DeleteAsync(int id);
    }
}
