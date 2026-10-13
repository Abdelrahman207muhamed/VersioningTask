using VersioningDemo.Api.Contracts.V1;
using VersioningDemo.Application.Products;

namespace VersioningDemo.Api.Mapping.V1
{
    public static class ProductMappings
    {
        public static ProductResponse ToV1 (this ProductDto d)
            => new (d.Id,d.Name, d.Price);
    
    }
}
