namespace JobSearch.Domains.Entities
{
    public class Employer
    {
        public int EmployerId { get; set; }

        public string CompanyName { get; set; }

        public string? Description { get; set; }

        public string Email { get; set; }

        public List<Vacancy> Vacancies { get; set; } = new();
    }
}