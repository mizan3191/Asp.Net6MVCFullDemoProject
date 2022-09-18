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
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CategoryDbContext _dbContext;
        public ProductRepository(CategoryDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Product product)
        {
            var productRepository = _dbContext.Products.FirstOrDefault(x => x.Id == product.Id);

            if(productRepository != null)
            {
                productRepository.Price = product.Price;
                productRepository.CategoryId = product.CategoryId;
                productRepository.Description = product.Description;
                productRepository.Name = product.Name;
            }
        }
    }
}
