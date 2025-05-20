using JobSearch.Domains.Entities;

namespace JobSearch.Domains.ValueObjects
{/// <summary>
 /// DTO для регистрации пользователя.
 /// </summary>
    public class RegistrationDto
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

        /// <summary>
        /// Пароль пользователя.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Дата рождения пользователя.
        /// </summary>
        public DateOnly? DateofBirth { get; set; }
        public User ToUser()
        {
            return new User
            {
                Name = this.Name,
                Phone = this.Phone,
                Email = this.Email,
                Password = this.Password,
                DateofBirth = this.DateofBirth
            };
        }

    }

}
