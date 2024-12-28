using App.Services.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Categories.Dto
{
    public record CategoryWithproductsDto(int Id, string Name, List<ProductDto> Products);
}
