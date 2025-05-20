namespace JobSearch.Domains.Entities
{
    /// <summary>
    /// Представляет сущность резюме пользователя в системе поиска работы.
    /// </summary>
    public class Resume
    {
        /// <summary>
        /// Уникальный идентификатор резюме.
        /// </summary>
        public int ResumeId { get; set; }

        /// <summary>
        /// Идентификатор пользователя, которому принадлежит резюме.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Заголовок резюме.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Информация об образовании пользователя.
        /// </summary>
        public string? Education { get; set; }

        /// <summary>
        /// Опыт работы пользователя.
        /// </summary>
        public required string Experience { get; set; }

        /// <summary>
        /// Дата рождения пользователя (вычисляемое свойство).
        /// </summary>
        public DateOnly? DateofBirth => User?.DateofBirth;

        /// <summary>
        /// Навигационное свойство для связи с пользователем.
        /// </summary>
        public User User { get; set; }
    }
}