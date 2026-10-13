using System;
using System.Collections.Generic;
using System.Text;

namespace VersioningDemo.Application.Products
{
    public record ProductDto
    (int Id ,
     string Name,
     decimal Price,
     string CategoryName);
}
