using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;
using JobSearch.Domains.ValueObjects;

namespace JobSearch.Storage
{
    public class Repository : IRepository
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // User methods
        public async Task<User> CreateUserAsync(RegistrationDto newUserDto)
        {
            var user = newUserDto.ToUser();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }
        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            User existingUser = await _context.Users.FindAsync(user.UserId);
            if (existingUser == null)
            {
                throw new KeyNotFoundException($"User with ID {user.UserId} not found");
            }

            _context.Entry(existingUser).CurrentValues.SetValues(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            User user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        // Resume methods
        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            bool userExists = await _context.Users
                .AnyAsync(u => u.UserId == resume.UserId);

            if (!userExists)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (string.IsNullOrWhiteSpace(resume.Title))
            {
                throw new ArgumentException("Title is required");
            }

            if (string.IsNullOrWhiteSpace(resume.Experience))
            {
                throw new ArgumentException("Experience is required");
            }

            await _context.Resume.AddAsync(resume);
            await _context.SaveChangesAsync();
            return await _context.Resume
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.ResumeId == resume.ResumeId);
        }

        public async Task<Resume> GetResumeByIdAsync(int id)
        {
            return await _context.Resume
                .Include(resume => resume.User)
                .FirstOrDefaultAsync(resume => resume.ResumeId == id);
        }

        public async Task UpdateResumeAsync(Resume resume)
        {
            Resume existingResume = await _context.Resume.FindAsync(resume.ResumeId);
            if (existingResume == null)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            _context.Entry(existingResume).CurrentValues.SetValues(resume);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResumeAsync(int id)
        {
            Resume resume = await _context.Resume.FindAsync(id);
            if (resume == null)
            {
                throw new KeyNotFoundException("Resume not found");
            }

            _context.Resume.Remove(resume);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Resume>> GetResumesByUserAsync(int userId)
        {
            return await _context.Resume
                .Where(resume => resume.UserId == userId)
                .ToListAsync();
        }
        public async Task<IEnumerable<Resume>> GetAllResumesAsync()
        {
            return await _context.Resume.ToListAsync();
        }

        // Employer methods
        public async Task<Employer> CreateEmployerAsync(Employer employer)
        {
            await _context.Employer.AddAsync(employer);
            await _context.SaveChangesAsync();
            return employer;
        }

        public async Task<Employer> GetEmployerByIdAsync(int id)
        {
            return await _context.Employer.FindAsync(id);
        }

        public async Task UpdateEmployerAsync(Employer employer)
        {
            _context.Entry(employer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<Employer> DeleteEmployerAsync(int id)
        {
            Employer employer = await _context.Employer.FindAsync(id);
            if (employer == null)
            {
                throw new KeyNotFoundException("Employer not found");
            }

            _context.Employer.Remove(employer);
            await _context.SaveChangesAsync();
            return employer;
        }

        // Responce methods
        public async Task<Responce> CreateResponceAsync(Responce responce)
        {
            var userExists = await _context.Users.AnyAsync(u => u.UserId == responce.UserId);
            var vacancyExists = await _context.Vacancy.AnyAsync(v => v.VacancyId == responce.VacancyId);

            if (!userExists || !vacancyExists)
                throw new KeyNotFoundException("User or Vacancy not found");

            await _context.Responce.AddAsync(responce);
            await _context.SaveChangesAsync();
            return responce;
        }

        public async Task<Responce> GetResponceByIdAsync(int id)
        {
            return await _context.Responce
                .Include(r => r.User)
                .Include(r => r.Vacancy)
                .FirstOrDefaultAsync(r => r.ResponceId == id);
        }

        public async Task UpdateResponceAsync(Responce responce)
        {
            var existing = await _context.Responce.FindAsync(responce.ResponceId);
            if (existing == null) throw new KeyNotFoundException("Responce not found");

            existing.CoverLetter = responce.CoverLetter;
            existing.Status = responce.Status;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResponceAsync(int id)
        {
            var responce = await _context.Responce.FindAsync(id);
            if (responce == null) throw new KeyNotFoundException("Responce not found");

            _context.Responce.Remove(responce);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Responce>> GetAllResponcesAsync()
        {
            return await _context.Responce.ToListAsync();
        }

        public async Task<List<Responce>> GetResponcesByUserAsync(int userId)
        {
            return await _context.Responce
                .Where(r => r.UserId == userId)
                .Include(r => r.Vacancy)
                .ToListAsync();
        }
    


        // Vacancy methods
        public async Task<Vacancy> CreateVacancyAsync(Vacancy vacancy)
        {
            await _context.Vacancy.AddAsync(vacancy);
            await _context.SaveChangesAsync();
            return vacancy;
        }
        public async Task<IEnumerable<Vacancy>> GetAllVacanciesAsync()
        {
            return await _context.Vacancy.ToListAsync();
        }
        public async Task<Vacancy> GetVacancyByIdAsync(int id)
        {
            return await _context.Vacancy.FindAsync(id);
        }

        public async Task UpdateVacancyAsync(Vacancy vacancy)
        {
            _context.Entry(vacancy).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVacancyAsync(int id)
        {
            Vacancy vacancy = await _context.Vacancy.FindAsync(id);
            if (vacancy == null)
            {
                throw new KeyNotFoundException("Vacancy not found");
            }

            _context.Vacancy.Remove(vacancy);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Vacancy>> GetByEmployerAsync(int employerId)
        {
            return await _context.Vacancy
                .Where(vacancy => vacancy.EmployerId == employerId)
                .ToListAsync();
        }
    }
}