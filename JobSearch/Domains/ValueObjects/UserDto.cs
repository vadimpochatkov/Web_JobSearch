namespace JobSearch.Domains.ValueObjects
{
    public class UserDto
    {
        public required string Name { get; set; }
        public string? Phone { get; set; }
        public required string Email { get; set; }
    }
}
