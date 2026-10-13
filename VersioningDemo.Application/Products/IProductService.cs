using System;
using System.Collections.Generic;
using System.Text;

namespace VersioningDemo.Application.Products
{
    public interface IProductService
    {
        Task<IReadOnlyList<ProductDto>> GetAllAsync(CancellationToken ct);
        Task<ProductDto?> GetByIdAsync(int id, CancellationToken ct);
    }
}
