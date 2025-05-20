using System.Text.Json.Serialization;

namespace JobSearch.Domains.Entities
{
    /// <summary>
    /// Представляет сущность вакансии.
    /// Содержит информацию о предложении работы, включая требования и условия.
    /// </summary>
    public class Vacancy
    {
        /// <summary>
        /// Уникальный идентификатор вакансии.
        /// </summary>
        public int VacancyId { get; set; }

        /// <summary>
        /// Идентификатор работодателя, опубликовавшего вакансию.
        /// </summary>
        public int EmployerId { get; set; }

        /// <summary>
        /// Название вакансии.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Подробное описание вакансии.
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

        /// <summary>
        /// Навигационное свойство для связи с работодателем.
        /// </summary>

        [JsonIgnore]
        public Employer Employer { get; set; }
    }
}
