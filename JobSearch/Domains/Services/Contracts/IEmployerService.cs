using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    /// <summary>
    /// Предоставляет функциональность для управления работодателями в системе.
    /// </summary>
    public interface IEmployerService
    {
        /// <summary>
        /// Создает нового работодателя в системе.
        /// </summary>
        /// <param name="dto">Данные для создания работодателя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит созданного работодателя.</returns>
        Task<Employer> CreateAsync(EmployerRequest dto);

        /// <summary>
        /// Получает работодателя по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор работодателя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит работодателя, если он найден.</returns>
        Task<Employer> GetByIdAsync(int id);

        /// <summary>
        /// Обновляет информацию о работодателе.
        /// </summary>
        /// <param name="id">Уникальный идентификатор работодателя.</param>
        /// <param name="dto">Данные для обновления работодателя.</param>
        /// <returns>Результат задачи содержит обновленного работодателя.</returns>
        Task UpdateAsync(int id, EmployerRequest dto);

        /// <summary>
        /// Удаляет работодателя из системы.
        /// </summary>
        /// <param name="id">Уникальный идентификатор работодателя.</param>
        /// <returns>Результат задачи содержит удаленного работодателя</returns>
        Task DeleteAsync(int id);
    }
}
