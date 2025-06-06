﻿namespace JobSearch.Domains.ValueObjects
{
    /// <summary>
    /// Представляет значение для создания или обновления отклика на вакансию.
    /// </summary>
    public class ResponseRequest
    {
        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
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
        public int VacancyId { get; set; }
    }
}