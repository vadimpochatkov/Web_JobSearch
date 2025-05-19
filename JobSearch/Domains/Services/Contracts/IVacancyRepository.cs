using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IVacancyRepository
    {
        Task<Vacancy> CreateVacancyAsync(Vacancy vacancy);
        Task<Vacancy> GetVacancyByIdAsync(int id);
        Task UpdateVacancyAsync(Vacancy vacancy);
        Task DeleteVacancyAsync(int id);
        Task<List<Vacancy>> GetByEmployerAsync(int employerId);
        Task<IEnumerable<Vacancy>> GetAllVacanciesAsync();
    }
}
