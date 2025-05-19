using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.UseCases
{
    public class ResponceService : IResponceService
    {
        private readonly IResponceRepository _repository;

        public ResponceService(IResponceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Responce> CreateAsync(ResponceDto dto)
        {
            var responce = new Responce
            {
                UserId = dto.UserId,
                VacancyId = dto.VacancyId,
                CoverLetter = dto.CoverLetter,
                Status = "Pending"
            };
            return await _repository.CreateResponceAsync(responce);
        }

        public async Task<IEnumerable<Responce>> GetAllAsync()
        {
            return await _repository.GetAllResponcesAsync();
        }

        public async Task<Responce> GetByIdAsync(int id)
        {
            return await _repository.GetResponceByIdAsync(id);
        }

        public async Task UpdateAsync(int id, ResponceDto dto)
        {
            var responce = await _repository.GetResponceByIdAsync(id);
            responce.CoverLetter = dto.CoverLetter;
            await _repository.UpdateResponceAsync(responce);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteResponceAsync(id);
        }
    }
}
