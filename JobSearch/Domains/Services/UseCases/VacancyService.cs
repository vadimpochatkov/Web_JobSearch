using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.UseCases
{
    public class VacancyService : IVacancyService
    {
        private readonly IRepository _repository;

        public VacancyService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Vacancy> CreateAsync(VacancyDto dto)
        {
            var vacancy = new Vacancy
            {
                EmployerId = dto.EmployerId,
                Title = dto.Title,
                Description = dto.Description,
                SalaryRange = dto.SalaryRange,
                Location = dto.Location
            };
            return await _repository.CreateVacancyAsync(vacancy);
        }

        public async Task<IEnumerable<Vacancy>> GetAllAsync()
        {
            return await _repository.GetAllVacanciesAsync();
        }

        public async Task<Vacancy> GetByIdAsync(int id)
        {
            return await _repository.GetVacancyByIdAsync(id);
        }

        public async Task UpdateAsync(int id, VacancyDto dto)
        {
            var vacancy = await _repository.GetVacancyByIdAsync(id);
            vacancy.Title = dto.Title;
            vacancy.Description = dto.Description;
            vacancy.SalaryRange = dto.SalaryRange;
            vacancy.Location = dto.Location;
            await _repository.UpdateVacancyAsync(vacancy);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteVacancyAsync(id);
        }
    }
}
