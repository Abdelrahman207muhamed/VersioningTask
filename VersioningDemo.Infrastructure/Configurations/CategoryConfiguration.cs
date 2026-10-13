using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VersioningDemo.Domain.Entities;

namespace VersioningDemo.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.HasData(new Category { Id = 1, Name = "Electronics" }, 
                            new Category { Id = 2, Name = "Accessories" });
        }
    }
}
