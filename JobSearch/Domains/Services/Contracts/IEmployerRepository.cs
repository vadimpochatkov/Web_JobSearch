using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IEmployerRepository
    {
        Task<Employer> CreateEmployerAsync(Employer employer);
        Task<Employer> GetEmployerByIdAsync(int id);
        Task UpdateEmployerAsync(Employer employer);
        Task<Employer> DeleteEmployerAsync(int id);
    }
}