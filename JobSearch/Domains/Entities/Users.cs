using System.Text.Json.Serialization;

namespace JobSearch.Domains.Entities
{
    public class User
    {
        [JsonIgnore]
        public int UserId { get; set; }

        public string Name { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateOnly? DateofBirth { get; set; }


    }
}