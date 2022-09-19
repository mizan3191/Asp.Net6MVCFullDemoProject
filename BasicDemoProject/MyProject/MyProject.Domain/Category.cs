using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.Domain
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int? DisplayOrder { get; set; }
        public string? Name { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;

    }
}
