using System.ComponentModel.DataAnnotations;

namespace MyProject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int DisplayOrder { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }= DateTime.Now;

    }
}
