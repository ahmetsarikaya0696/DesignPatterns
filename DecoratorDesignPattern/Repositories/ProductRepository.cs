using DecoratorDesignPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace DecoratorDesignPattern.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _applicationDbContext.Products.AddAsync(product);
            await _applicationDbContext.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _applicationDbContext.Products.ToListAsync();
        }

        public async Task<List<Product>> GetAllAsync(string userId)
        {
            return await _applicationDbContext.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Products.FindAsync(id);
        }

        public async Task RemoveAsync(Product product)
        {
            _applicationDbContext.Remove(product);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _applicationDbContext.Update(product);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
