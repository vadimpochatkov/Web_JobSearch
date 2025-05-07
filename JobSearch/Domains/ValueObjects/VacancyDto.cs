namespace JobSearch.Domains.ValueObjects
{
    public class VacancyDto
    {
        public int EmployerId { get; set; } 
        public required string Title { get; set; }
        public required string Description { get; set; }
        public string? SalaryRange { get; set; }
        public string? Location { get; set; }
    }
}