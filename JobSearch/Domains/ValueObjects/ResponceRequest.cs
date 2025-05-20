using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json.Serialization;

namespace JobSearch.Domains.ValueObjects
{
    public class ResponseDto
    {
        public int UserId { get; set; }
        public string? CoverLetter { get; set; }
        public int? ResumeId { get; set; }

        [JsonIgnore]
        [BindNever]
        public int VacancyId { get; set; }
    }
}
