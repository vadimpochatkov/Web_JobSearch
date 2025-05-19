using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;

namespace JobSearch.Storage
{
    public class ResponseRepository : IResponseRepository
    {
        private readonly ApplicationDbContext _context;

        public ResponseRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Responce> CreateResponceAsync(Responce responce)
        {
            var userExists = await _context.User.AnyAsync(u => u.UserId == responce.UserId);
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
    }
}