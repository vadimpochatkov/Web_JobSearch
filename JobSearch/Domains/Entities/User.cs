using System.Text.Json.Serialization;

namespace JobSearch.Domains.Entities
{
    /// <summary>
    /// Представляет сущность пользователя системы поиска работы.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Уникальный идентификатор пользователя в системе.
        /// </summary>
        [JsonIgnore]
        public int UserId { get; set; }

        /// <summary>
        /// Полное имя пользователя.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Контактный номер телефона пользователя.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Дата рождения пользователя.
        /// </summary>
        /// <remarks>
        /// Формат: ГГГГ-ММ-ДД.
        /// </remarks>
        public DateOnly? DateofBirth { get; set; }
    }
}