using MyProject.DataAccess.Contexts;
using MyProject.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProject.DataAccess.UnitOfWorks
{
    public class UnitOfWorks : IUnitOfWorks
    {
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }

        private readonly ApplicationDbContext _dbContext;

        public UnitOfWorks(ApplicationDbContext dbContext)
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
