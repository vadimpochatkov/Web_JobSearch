
namespace JobSearch.Domains.Entities
{
    public class Vacancy
    {
        public int VacancyId { get; set; }

        public int EmployerId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? SalaryRange { get; set; }

        public string? Location { get; set; }

        public Employer Employer { get; set; }

    }
}