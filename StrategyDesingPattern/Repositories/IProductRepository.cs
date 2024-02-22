using StrategyDesingPattern.Models;

namespace StrategyDesingPattern.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetById(string id);
        Task<List<Product>> GetAllByUserId(string userId);
        Task<Product> Create(Product product);
        Task Update(Product product);
        Task Delete(Product product);
    }
}
