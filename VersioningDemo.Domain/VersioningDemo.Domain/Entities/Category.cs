using System;
using System.Collections.Generic;
using System.Text;

namespace VersioningDemo.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }= default!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
