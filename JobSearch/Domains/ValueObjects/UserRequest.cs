namespace JobSearch.Domains.ValueObjects
{
    /// <summary>
    /// Передача данных о пользователе.
    /// </summary>
    public class UserRequest
    {
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
    }
}
