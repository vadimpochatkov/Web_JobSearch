using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;

namespace JobSearch.Storage
{
    public class EmployerRepository : IEmployerRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployerRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Employer> CreateEmployerAsync(Employer employer)
        {
            await _context.Employer.AddAsync(employer);
            await _context.SaveChangesAsync();
            return employer;
        }

        public async Task<Employer> GetEmployerByIdAsync(int id)
        {
            return await _context.Employer
            .Include("Vacancies") 
            .FirstOrDefaultAsync(e => e.EmployerId == id);
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
    }
}