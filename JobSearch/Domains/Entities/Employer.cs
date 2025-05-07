namespace JobSearch.Domains.Entities
{
    public class Employer
    {
        public int EmployerId { get; set; }

        public required string CompanyName { get; set; }

        public string? Description { get; set; }

        public required string Email { get; set; }

        public List<Vacancy> Vacancies { get; set; } = new();
    }
}