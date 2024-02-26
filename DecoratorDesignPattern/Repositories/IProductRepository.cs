using DecoratorDesignPattern.Models;

namespace DecoratorDesignPattern.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllAsync(string userId);
        Task<Product> AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task RemoveAsync(Product product);
    }
}
