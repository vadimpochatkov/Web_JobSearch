namespace JobSearch.Domains.ValueObjects
{
    /// <summary>
    /// Представляет значение для передачи данных о работодателе.
    /// </summary>

    public class EmployerRequest
    {
        /// <summary>
        /// Название компании работодателя.
        /// </summary>
        public required string CompanyName { get; set; }

        /// <summary>
        /// Контактный email работодателя.
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Описание компании.
        /// </summary>
        public string? Description { get; set; }
    }
}