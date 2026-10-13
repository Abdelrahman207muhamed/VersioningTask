using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VersioningDemo.Api.Contracts.V2;
using VersioningDemo.Api.Mapping.V1;
using VersioningDemo.Api.Mapping.V2;
using VersioningDemo.Application.Products;

namespace VersioningDemo.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductsController(IProductService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAll(CancellationToken ct)
        => Ok((await service.GetAllAsync(ct)).Select(p => p.ToV2()));

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id, CancellationToken ct)
        {
            var product = await service.GetByIdAsync(id, ct);
            return product is null ? NotFound() : Ok(product.ToV2());
        }
    }
}
