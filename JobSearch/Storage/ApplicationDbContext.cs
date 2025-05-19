using Microsoft.EntityFrameworkCore;

using JobSearch.Domains.Entities;

namespace JobSearch.Storage
{
   public class ApplicationDbContext : DbContext
   {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Responce> Responce { get; set; }
    }
}



