using JobSearch.Domains.Entities;
using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.UseCases
{
    public class ResponseService : IResponseService
    {
        private readonly IResponseRepository _repository;

        public ResponseService(IResponseRepository repository)
        {
            _repository = repository;
        }
        public async Task<Responce> CreateAsync(ResponseRequest newresponse)
        {
            var response = new Responce
            {
                UserId = newresponse.UserId,
                VacancyId = newresponse.VacancyId,
                CoverLetter = newresponse.CoverLetter,
                Status = "Pending"
            };
            return await _repository.CreateResponceAsync(response);
        }
        public async Task<Responce> CreateAsync(ResponseDto dto)
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

        public async Task UpdateAsync(int id, ResponseRequest updateresponse)
        {
            var responce = await _repository.GetResponceByIdAsync(id);
            responce.CoverLetter = updateresponse.CoverLetter;
            await _repository.UpdateResponceAsync(responce);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteResponceAsync(id);
        }
    }
}
