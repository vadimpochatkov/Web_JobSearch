using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace JobSearch.Domains.ValueObjects
{
    public class ResponseDto
    {
        /// <summary>
        /// Идентификатор отклика.
        /// </summary>
        public int ResponseId { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Идентификатор резюме, прикрепленного к отклику.
        /// </summary>
        public int? ResumeId { get; set; }

        /// <summary>
        /// Идентификатор вакансии, на которую отправляется отклик.
        /// </summary>
        public int VacancyId { get; set; }  // убрал JsonIgnore и BindNever

        /// <summary>
        /// Идентификатор работодателя, которому отправляется письмо.
        /// </summary>
        public int EmployerId { get; set; } // добавил поле для работодателя
    }
}

