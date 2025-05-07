namespace JobSearch.Domains.Entities
{
    public class Responce
    {
        public int ResponceId { get; set; }
        public int VacancyId { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } = "pending";
        public DateTime ResponceDate { get; set; } = DateTime.UtcNow;
        public string? CoverLetter { get; set; }

        public  Vacancy Vacancy { get; set; }
        public  User User { get; set; }
    }
}