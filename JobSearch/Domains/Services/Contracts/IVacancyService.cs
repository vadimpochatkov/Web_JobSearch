using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    /// <summary>
    /// Предоставляет функциональность для управления вакансиями в системе.
    /// </summary>
    public interface IVacancyService
    {
        /// <summary>
        /// Создает новую вакансию в системе.
        /// </summary>
        /// <param name="dto">Данные для создания вакансии.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит созданную вакансию.</returns>
        Task<Vacancy> CreateAsync(VacancyRequest dto);

        /// <summary>
        /// Получает список всех вакансий в системе.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит коллекцию вакансий.</returns>
        Task<IEnumerable<Vacancy>> GetAllAsync();

        /// <summary>
        /// Получает вакансию по её уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит вакансию, если она найдена.</returns>
        Task<Vacancy> GetByIdAsync(int id);

        /// <summary>
        /// Обновляет информацию о вакансии.
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии.</param>
        /// <param name="dto">Данные для обновления вакансии.</param>
        /// <returns>Результат задачи содержит обновленную вакансию.</returns>
        Task UpdateAsync(int id, VacancyRequest dto);

        /// <summary>
        /// Удаляет вакансию из системы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор вакансии.</param>
        /// <returns>Результат задачи содержит удаленную вакансию.</returns>
        Task DeleteAsync(int id);
    }
}
