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
        public async Task<Response> CreateResponseAsync(Response response)
        {
            var userExists = await _context.User.AnyAsync(u => u.UserId == response.UserId);
            var vacancyExists = await _context.Vacancy.AnyAsync(v => v.VacancyId == response.VacancyId);

            if (!userExists || !vacancyExists)
                throw new KeyNotFoundException("User or Vacancy not found");

            await _context.Response.AddAsync(response);
            await _context.SaveChangesAsync();
            return response;
        }

        public async Task<Response> GetResponseByIdAsync(int id)
        {
            return await _context.Response
                .Include(r => r.User)
                .Include(r => r.Vacancy)
                .FirstOrDefaultAsync(r => r.ResponseId == id);
        }

        public async Task UpdateResponseAsync(Response response)
        {
            var existing = await _context.Response.FindAsync(response.ResponseId);
            if (existing == null) throw new KeyNotFoundException("Responce not found");

            existing.CoverLetter = response.CoverLetter;
            existing.Status = response.Status;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResponseAsync(int id)
        {
            var response = await _context.Response.FindAsync(id);
            if (response == null) throw new KeyNotFoundException("Responce not found");

            _context.Response.Remove(response);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Response>> GetAllResponsesAsync()
        {
            return await _context.Response.ToListAsync();
        }

        public async Task<List<Response>> GetResponsesByUserAsync(int userId)
        {
            return await _context.Response
                .Where(r => r.UserId == userId)
                .Include(r => r.Vacancy)
                .ToListAsync();
        }
    }
}