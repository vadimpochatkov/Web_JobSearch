using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    /// <summary>
    /// Предоставляет функциональность для управления пользователями системы.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Получает пользователя по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит пользователя, если он найден.</returns>
        Task<User> GetByIdAsync(int id);

        /// <summary>
        /// Обновляет информацию о пользователе.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <param name="dto">Данные для обновления пользователя.</param>
        /// <returns>Обновленный пользователь.</returns>
        Task UpdateAsync(int id, UserRequest dto);

        /// <summary>
        /// Удаляет пользователя из системы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Удаленный пользователь.</returns>
        Task DeleteAsync(int id);
    }
}
