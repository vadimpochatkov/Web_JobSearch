namespace JobSearch.Domains.ValueObjects
{
    public class ResponceDto
    {
        public int UserId { get; set; }
        public int VacancyId { get; set; }
        public string? CoverLetter { get; set; }  
        public int? ResumeId { get; set; }       
    }
}
