using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VersioningDemo.Api.Swagger
{
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    : IConfigureOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var d in provider.ApiVersionDescriptions)
                options.SwaggerDoc(d.GroupName, new OpenApiInfo
                {
                    Title = "Versioning Demo API",
                    Version = d.ApiVersion.ToString(),
                    Description = d.IsDeprecated ? "This version is deprecated." : null
                });
        }
    }
}
