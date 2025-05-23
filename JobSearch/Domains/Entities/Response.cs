namespace JobSearch.Domains.Entities
{
    /// <summary>
    /// Представляет сущность отклика на вакансию.
    /// Содержит информацию о взаимодействии соискателя с вакансией.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Уникальный идентификатор отклика.
        /// </summary>
        public int ResponseId { get; set; }

        /// <summary>
        /// Идентификатор вакансии, на которую отправлен отклик.
        /// </summary>
        public int VacancyId { get; set; }

        /// <summary>
        /// Идентификатор пользователя-соискателя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Текущий статус отклика.
        /// </summary>
        public string Status { get; set; } = "pending";

        /// <summary>
        /// Дата и время создания отклика.
        /// </summary>
        public DateTime ResponseDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Сопроводительное письмо, прикрепленное к отклику.
        /// </summary>
        public string? CoverLetter { get; set; }

        /// <summary>
        /// Идентификатор резюме, прикрепленного к отклику.
        /// </summary>
        public int? ResumeId { get; set; }

        public Vacancy Vacancy { get; set; }

        public User User { get; set; }

        public Resume Resume { get; set; }
    }
}