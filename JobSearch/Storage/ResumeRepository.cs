using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Services.Contracts;
using JobSearch.Domains.Entities;

namespace JobSearch.Storage
{
    public class ResumeRepository : IResumeRepository
    {
        private readonly ApplicationDbContext _context;

        public ResumeRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        // Resume methods
        public async Task<Resume> CreateResumeAsync(Resume resume)
        {
            bool userExists = await _context.User
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
    }
}