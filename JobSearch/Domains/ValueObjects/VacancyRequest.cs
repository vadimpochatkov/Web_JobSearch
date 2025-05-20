namespace JobSearch.Domains.ValueObjects
{
    /// <summary>
    /// Представляет значение для создания или обновления вакансии.
    /// </summary>
    public class VacancyRequest
    {
        /// <summary>
        /// Идентификатор работодателя, публикующего вакансию.
        /// </summary>
        public int EmployerId { get; set; }

        /// <summary>
        /// Название вакансии.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Подробное описание вакансии и требований.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// Диапазон зарплаты.
        /// </summary>
        public string? SalaryRange { get; set; }

        /// <summary>
        /// Местоположение работы.
        /// </summary>
        public string? Location { get; set; }
    }
}