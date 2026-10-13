using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Domain.Entities;

namespace VersioningDemo.Infrastructure.Persistence
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder b)
            => b.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
