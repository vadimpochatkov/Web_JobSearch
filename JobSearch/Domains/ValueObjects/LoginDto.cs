namespace JobSearch.Domains.ValueObjects
{
    /// <summary>
    /// DTO для авторизации пользователя .
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Адрес электронной почты пользователя.
        /// </summary>
        public required string Email { get; set; }
        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public required string Password { get; set; }
    }
}