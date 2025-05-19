using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;

namespace JobSearch.Storage
{
    public class VacancyRepository : IVacancyRepository
    {
        private readonly ApplicationDbContext _context;

        public VacancyRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
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