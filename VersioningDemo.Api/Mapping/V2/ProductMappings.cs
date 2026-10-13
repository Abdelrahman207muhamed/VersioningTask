using VersioningDemo.Api.Contracts.V2;
using VersioningDemo.Application.Products;

namespace VersioningDemo.Api.Mapping.V2
{
    public static class ProductMappings
    {
        public static ProductResponse ToV2(this ProductDto d) => new(d.Id, d.Name, d.Price, d.CategoryName);
    }
}
