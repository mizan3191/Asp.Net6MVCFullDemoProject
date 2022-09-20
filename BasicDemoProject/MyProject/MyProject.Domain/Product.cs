using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Domain
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        [ValidateNever]
        public string? ImageUrl { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public byte[]? FileData { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? CategoryId { get; set; }
        [ValidateNever]
        public Category? Category { get; set; }
    }
}
