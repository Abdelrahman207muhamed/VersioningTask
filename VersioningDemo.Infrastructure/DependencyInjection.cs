using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Application.Abstractions;
using VersioningDemo.Infrastructure.Persistence;
using VersioningDemo.Infrastructure.Repositories;

namespace VersioningDemo.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(o =>
                o.UseSqlServer(config.GetConnectionString("Default")));
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
