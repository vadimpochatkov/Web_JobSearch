using JobSearch.Domains.Entities;

namespace JobSearch.Domains.Services.Contracts
{
    /// <summary>
    /// Предоставляет функциональность для отправки электронных писем в системе.
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправляет уведомление о новом отклике на вакансию.
        /// </summary>
        /// <param name="response">Данные отклика на вакансию.</param>
        /// <returns>Задача, представляющая асинхронную операцию отправки электронного письма.</returns>
        Task SendVacancyApplicationEmailAsync(Response response, Vacancy vacancy, Employer employer, User user, Resume? resume = null);
    }
}