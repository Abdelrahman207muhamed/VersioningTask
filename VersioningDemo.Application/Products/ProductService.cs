using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Application.Abstractions;

namespace VersioningDemo.Application.Products
{
    public class ProductService(IProductRepository repo) : IProductService
    {
        public async Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken ct)
        {
            var products = await repo.GetAllProductsAsync(ct);
            return products.Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Price,
                p.Category.Name))
                .ToList();
        }

        public async Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct)
        {
            var p = await repo.GetProductsByIdAsync(id, ct);
            return p is null ? null : new ProductDto(p.Id, p.Name, p.Price, p.Category.Name);
        }
    }
}
