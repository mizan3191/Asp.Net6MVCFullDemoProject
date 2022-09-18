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
    public class UnitOfWorks : IUnitOfWorks
    {
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }

        private readonly CategoryDbContext _dbContext;

        public UnitOfWorks(CategoryDbContext dbContext)
        {
            _dbContext = dbContext;
            CategoryRepository = new CategoryRepository(dbContext);
            ProductRepository = new ProductRepository(dbContext);
        }
        
        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
