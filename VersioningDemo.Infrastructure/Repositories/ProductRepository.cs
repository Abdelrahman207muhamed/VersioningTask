using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Application.Abstractions;
using VersioningDemo.Domain.Entities;
using VersioningDemo.Infrastructure.Persistence;

namespace VersioningDemo.Infrastructure.Repositories
{
    public class ProductRepository(AppDbContext db) : IProductRepository
    {
        public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken ct)
        => await db.Products.AsNoTracking().Include(p => p.Category).ToListAsync(ct);

        public async Task<Product?> GetProductsByIdAsync(int id, CancellationToken ct)
        => await db.Products.AsNoTracking().Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id, ct);
    }
}
