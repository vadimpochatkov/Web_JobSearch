namespace JobSearch.Domains.ValueObjects
{
    public class VacancyDto
    {
        public int EmployerId { get; set; } 
        public string Title { get; set; }
        public string Description { get; set; }
        public string? SalaryRange { get; set; }
        public string? Location { get; set; }
    }
}