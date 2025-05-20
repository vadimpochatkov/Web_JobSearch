using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IResponseRepository
    {
        Task<Response> CreateResponseAsync(Response application);
        Task<Response> GetResponseByIdAsync(int id);
        Task UpdateResponseAsync(Response application);
        Task DeleteResponseAsync(int id);
        Task<List<Response>> GetResponsesByUserAsync(int userId);
        Task<IEnumerable<Response>> GetAllResponsesAsync();
    }
}