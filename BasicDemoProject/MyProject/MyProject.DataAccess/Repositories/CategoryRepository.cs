using MyProject.DataAccess.Contexts;
using MyProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccess.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
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