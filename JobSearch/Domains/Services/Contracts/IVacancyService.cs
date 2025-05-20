using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IVacancyService
    {
        Task<Vacancy> CreateAsync(VacancyRequest dto);
        Task<IEnumerable<Vacancy>> GetAllAsync();
        Task<Vacancy> GetByIdAsync(int id);
        Task UpdateAsync(int id, VacancyRequest dto);
        Task DeleteAsync(int id);
    }
}
