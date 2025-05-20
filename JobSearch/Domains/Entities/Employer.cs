namespace JobSearch.Domains.Entities
{
    /// <summary>
    /// Представляет сущность работодателя.
    /// Содержит информацию о компании-работодателе и связанных с ней вакансиях.
    /// </summary>
    public class Employer
    {
        /// <summary>
        /// Уникальный идентификатор работодателя.
        /// </summary>
        public int EmployerId { get; set; }

        /// <summary>
        /// Название компании.
        /// </summary>
        public required string CompanyName { get; set; }

        /// <summary>
        /// Описание компании.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Контактный email работодателя.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Список вакансий, опубликованных работодателем.
        /// </summary>
        public List<Vacancy> Vacancies { get; set; } = new();
    }
}