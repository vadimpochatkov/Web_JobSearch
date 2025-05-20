using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IEmailService
    {
        Task SendVacancyApplicationEmailAsync(Responce response);
    }
}