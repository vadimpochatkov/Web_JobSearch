using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.UseCases
{
    public class EmployerService : IEmployerService
    {
        private readonly IEmployerRepository _repository;

        public EmployerService(IEmployerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Employer> CreateAsync(EmployerRequest dto)
        {
            var employer = new Employer
            {
                CompanyName = dto.CompanyName,
                Email = dto.Email,
                Description = dto.Description
            };
            return await _repository.CreateEmployerAsync(employer);
        }

        public async Task<Employer> GetByIdAsync(int id)
        {
            return await _repository.GetEmployerByIdAsync(id);
        }

        public async Task UpdateAsync(int id, EmployerRequest dto)
        {
            var employer = await _repository.GetEmployerByIdAsync(id);
            employer.CompanyName = dto.CompanyName;
            employer.Email = dto.Email;
            employer.Description = dto.Description;
            await _repository.UpdateEmployerAsync(employer);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteEmployerAsync(id);
        }
    }
}
