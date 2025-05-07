namespace JobSearch.Domains.ValueObjects
{
    public class EmployerDto
    {
        public required string CompanyName { get; set; }
        public required string Email { get; set; }
        public string? Description { get; set; }
    }
}