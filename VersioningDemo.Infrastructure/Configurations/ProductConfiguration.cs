using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Domain.Entities;

namespace VersioningDemo.Infrastructure.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                   .HasMaxLength(200)
                   .IsRequired();

            builder.Property(p => p.Price)
                   .HasPrecision(18, 2);

            builder.HasOne(p=>p.Category)
                   .WithMany(c=>c.Products)
                   .HasForeignKey(p => p.CategoryId);
            
            builder.HasData(
                    new Product { Id = 1, Name = "Laptop", Price = 25000, CategoryId = 1 },
                    new Product { Id = 2, Name = "Mouse", Price = 500, CategoryId = 1 });


        }
    }
}
