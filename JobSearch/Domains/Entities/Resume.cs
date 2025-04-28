namespace JobSearch.Domains.Entities
{
    public class Resume
    {
        public int ResumeId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Education { get; set; }
        public string Experience { get; set; }

        public DateOnly? DateofBirth => User?.DateofBirth;

        public User User { get; set; }
    }
}