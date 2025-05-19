using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Domains.Services.Contracts
{
    public interface IRepository
    {
        // User methods
        Task<User> CreateUserAsync(RegistrationDto newuser);
        Task<User> GetUserByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
        Task<User> GetUserByEmailAsync(string email);

        // Resume methods
        Task<Resume> CreateResumeAsync(Resume resume);
        Task<Resume> GetResumeByIdAsync(int id);
        Task UpdateResumeAsync(Resume resume);
        Task DeleteResumeAsync(int id);
        Task<List<Resume>> GetResumesByUserAsync(int userId);
        Task<IEnumerable<Resume>> GetAllResumesAsync();

        // Employer methods
        Task<Employer> CreateEmployerAsync(Employer employer);
        Task<Employer> GetEmployerByIdAsync(int id);
        Task UpdateEmployerAsync(Employer employer);
        Task<Employer> DeleteEmployerAsync(int id);

        // Application methods
        Task<Responce> CreateResponceAsync(Responce application);
        Task<Responce> GetResponceByIdAsync(int id);
        Task UpdateResponceAsync(Responce application);
        Task DeleteResponceAsync(int id);
        Task<List<Responce>> GetResponcesByUserAsync(int userId);
        Task<IEnumerable<Responce>> GetAllResponcesAsync();

        // Vacancy methods
        Task<Vacancy> CreateVacancyAsync(Vacancy vacancy);
        Task<Vacancy> GetVacancyByIdAsync(int id);
        Task UpdateVacancyAsync(Vacancy vacancy);
        Task DeleteVacancyAsync(int id);
        Task<List<Vacancy>> GetByEmployerAsync(int employerId);
        Task<IEnumerable<Vacancy>> GetAllVacanciesAsync();
    }
}
