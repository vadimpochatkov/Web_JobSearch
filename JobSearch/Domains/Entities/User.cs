чusing System.Text.Json.Serialization;

namespace JobSearch.Domains.Entities
{
    public class User
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public required string Name { get; set; }

        public string? Phone { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public DateOnly? DateofBirth { get; set; }
    }
}