using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Domain.ViewModels
{
    public class ProductVM
    {
        public IFormFile? FormFile { get; set; }
        public Product? Product { get; set; } = new Product();
        [ValidateNever]
        public IEnumerable<Product>? Products { get; set; } = new List<Product>();
        [ValidateNever]
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
