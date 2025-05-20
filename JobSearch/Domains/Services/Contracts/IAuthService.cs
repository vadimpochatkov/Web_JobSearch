using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    /// <summary>
    /// Предоставляет функциональность аутентификации и управления пользователями.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Регистрирует нового пользователя в системе.
        /// </summary>
        /// <param name="newuser">Данные для регистрации, содержащие информацию о пользователе.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит зарегистрированного пользователя.</returns>
        Task<User> RegisterAsync(RegistrationDto newuser);

        /// <summary>
        /// Аутентифицирует пользователя и выполняет вход в систему.
        /// </summary>
        /// <param name="dto">Учетные данные для входа.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит аутентифицированного пользователя.</returns>
        Task<User> LoginAsync(LoginDto dto);

        /// <summary>
        /// Получает пользователя по его уникальному идентификатору.
        /// </summary>
        /// <param name="id">Уникальный идентификатор пользователя.</param>
        /// <returns>Задача, представляющая асинхронную операцию. Результат задачи содержит пользователя, если он найден.</returns>
        Task<User> GetUserByIdAsync(int id);
    }
}