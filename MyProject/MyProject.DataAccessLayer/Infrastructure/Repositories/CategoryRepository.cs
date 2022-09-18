using MyProject.DataAccessLayer.DbContexts;
using MyProject.DataAccessLayer.Infrastructure.IRepositories;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccessLayer.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly CategoryDbContext _dbContext;
        public CategoryRepository(CategoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
            var categoryRepository = _dbContext.Categories.FirstOrDefault(x => x.Id == category.Id);
           
            if(categoryRepository != null)
            {
                categoryRepository.DisplayOrder = category.DisplayOrder;
                categoryRepository.Name = category.Name;
            }
        }
    }
}