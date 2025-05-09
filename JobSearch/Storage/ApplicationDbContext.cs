﻿using JobSearch.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace JobSearch.Storage
{
   public class ApplicationDbContext : DbContext
   {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Employer> Employer { get; set; }
        public DbSet<Vacancy> Vacancy { get; set; }
        public DbSet<Resume> Resume { get; set; }
        public DbSet<Responce> Responce { get; set; }
    }
}



