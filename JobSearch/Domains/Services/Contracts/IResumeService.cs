using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts;
/// <summary>
/// Предоставляет функциональность для управления резюме в системе.
/// </summary>
public interface IResumeService
{
    /// <summary>
    /// Создает новое резюме в системе.
    /// </summary>
    /// <param name="dto">Данные для создания резюме.</param>
    /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит созданное резюме.</returns>
    Task<Resume> CreateResumeAsync(ResumeRequest dto);

    /// <summary>
    /// Получает список всех резюме в системе.
    /// </summary>
    /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит коллекцию резюме.</returns>
    Task<IEnumerable<Resume>> GetAllResumesAsync();

    /// <summary>
    /// Получает резюме по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор резюме.</param>
    /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит резюме, если оно найдено.</returns>
    Task<Resume> GetResumeByIdAsync(int id);

    /// <summary>
    /// Обновляет информацию о резюме.
    /// </summary>
    /// <param name="id">Уникальный идентификатор резюме.</param>
    /// <param name="dto">Данные для обновления резюме.</param>
    /// <returns>Результат задачи содержит обновленное резюме.</returns>
    Task UpdateResumeAsync(int id, ResumeRequest dto);

    /// <summary>
    /// Удаляет резюме из системы.
    /// </summary>
    /// <param name="id">Уникальный идентификатор резюме.</param>
    /// <returns>Результат задачи содержит удаленное резюме.</returns>
    Task DeleteResumeAsync(int id);
}