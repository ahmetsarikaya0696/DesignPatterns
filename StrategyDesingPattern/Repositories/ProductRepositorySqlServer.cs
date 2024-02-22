using Microsoft.EntityFrameworkCore;
using StrategyDesingPattern.Models;

namespace StrategyDesingPattern.Repositories
{
    public class ProductRepositorySqlServer : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepositorySqlServer(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> Create(Product product)
        {
            product.Id = Guid.NewGuid().ToString();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllByUserId(string userId)
        {
            return await _context.Products.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
