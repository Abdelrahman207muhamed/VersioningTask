using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Domain.Entities;

namespace VersioningDemo.Application.Abstractions
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken ct);
        Task<Product?> GetProductsByIdAsync(int id, CancellationToken ct);
    }
}
