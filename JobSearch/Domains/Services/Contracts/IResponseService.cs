using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    /// <summary>
    /// Предоставляет функциональность для управления откликами на вакансии в системе.
    /// </summary>
    public interface IResponseService
    {
        /// <summary>
        /// Создает новый отклик на вакансию.
        /// </summary>
        /// <param name="dto">Данные для создания отклика.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит созданный отклик.</returns>
        Task<Response> CreateAsync(ResponseDto dto);

        /// <summary>
        /// Создает новый отклик на вакансию.
        /// </summary>
        /// <param name="newresponse">Данные для создания отклика.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит созданный отклик.</returns>
        Task<Response> CreateAsync(ResponseRequest newresponse);

        /// <summary>
        /// Получает список всех откликов в системе.
        /// </summary>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит коллекцию откликов.</returns>
        Task<IEnumerable<Response>> GetAllAsync();

        /// <summary>
        /// Получает отклик по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор отклика.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит отклик, если он найден.</returns>
        Task<Response> GetByIdAsync(int id);

        /// <summary>
        /// Обновляет информацию об отклике.
        /// </summary>
        /// <param name="id">Уникальный идентификатор отклика.</param>
        /// <param name="updateresponse">Данные для обновления отклика.</param>
        /// <returns>Результат задачи содержит обновленный отклик.</returns>
        Task UpdateAsync(int id, ResponseRequest updateresponse);

        /// <summary>
        /// Удаляет отклик из системы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор отклика.</param>
        /// <returns>Результат задачи содержит удаленный отклик.</returns>
        Task DeleteAsync(int id);
    }
}
