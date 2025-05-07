
namespace JobSearch.Domains.Entities
{
    public class Vacancy
    {
        public int VacancyId { get; set; }

        public int EmployerId { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public string? SalaryRange { get; set; }

        public string? Location { get; set; }

        public required Employer Employer { get; set; }

    }
}