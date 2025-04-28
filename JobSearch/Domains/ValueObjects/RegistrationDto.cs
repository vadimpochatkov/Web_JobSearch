using JobSearch.Domains.Entities;

namespace JobSearch.Domains.ValueObjects
{
    public class RegistrationDto
    {
        public string Name { get; set; }

        public string? Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

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
