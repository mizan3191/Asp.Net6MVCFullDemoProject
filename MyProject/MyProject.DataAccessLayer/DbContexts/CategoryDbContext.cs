using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.DataAccessLayer.DbContexts
{
    public class CategoryDbContext : DbContext
    {
        public CategoryDbContext(DbContextOptions<CategoryDbContext> dbContext)
            : base(dbContext) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
