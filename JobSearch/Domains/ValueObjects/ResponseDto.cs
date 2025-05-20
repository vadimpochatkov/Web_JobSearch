using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace JobSearch.Domains.ValueObjects
{
    /// <summary>
    /// Представляет значение для создания или обновления отклика на вакансию.
    /// </summary>
    public class ResponseDto
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        // <summary>
        /// Сопроводительное письмо, прикрепленное к отклику.
        /// </summary>
        public string? CoverLetter { get; set; }

        /// <summary>
        /// Идентификатор резюме, прикрепленного к отклику.
        /// </summary>
        public int? ResumeId { get; set; }

        /// <summary>
        /// Идентификатор вакансии, на которую отправляется отклик.
        /// </summary>
        [JsonIgnore]
        [BindNever]
        public int VacancyId { get; set; }
    }
}
