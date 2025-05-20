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
        public async Task<Response> CreateAsync(ResponseRequest newresponse)
        {
            var response = new Response
            {
                UserId = newresponse.UserId,
                VacancyId = newresponse.VacancyId,
                CoverLetter = newresponse.CoverLetter,
                Status = "Pending"
            };
            return await _repository.CreateResponseAsync(response);
        }
        public async Task<  Response> CreateAsync(ResponseDto dto)
        {
            var responce = new Response
            {
                UserId = dto.UserId,
                VacancyId = dto.VacancyId,
                Status = "Pending"
            };
            return await _repository.CreateResponseAsync(responce);
        }

        public async Task<IEnumerable<Response>> GetAllAsync()
        {
            return await _repository.GetAllResponsesAsync();
        }

        public async Task<Response> GetByIdAsync(int id)
        {
            return await _repository.GetResponseByIdAsync(id);
        }

        public async Task UpdateAsync(int id, ResponseRequest updateresponse)
        {
            var responce = await _repository.GetResponseByIdAsync(id);
            responce.CoverLetter = updateresponse.CoverLetter;
            await _repository.UpdateResponseAsync(responce);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteResponseAsync(id);
        }
    }
}
