using JobSearch.Domains.Entities;

namespace JobSearch.Domains.ValueObjects
{
    public class RegistrationDto
    {
        public required string Name { get; set; }

        public string? Phone { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }

        public DateOnly? DateofBirth { get; set; }
        public User ToUser()
        {
            return new User
            {
                Name = this.Name,
                Phone = this.Phone,
                Email = this.Email,
                Password = this.Password,
                DateofBirth = this.DateofBirth
            };
        }

    }

}
