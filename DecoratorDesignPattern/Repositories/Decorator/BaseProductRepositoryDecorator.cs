using DecoratorDesignPattern.Models;

namespace DecoratorDesignPattern.Repositories.Decorator
{
    public abstract class BaseProductRepositoryDecorator : IProductRepository
    {
        private readonly IProductRepository _productRepository;

        protected BaseProductRepositoryDecorator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public virtual Task<Product> AddAsync(Product product)
        {
            return _productRepository.AddAsync(product);
        }

        public virtual async Task<List<Product>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public virtual Task<List<Product>> GetAllAsync(string userId)
        {
            return _productRepository.GetAllAsync(userId);
        }

        public virtual async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public virtual async Task RemoveAsync(Product product)
        {
            await _productRepository.RemoveAsync(product);
        }

        public virtual async Task UpdateAsync(Product product)
        {
            await _productRepository.UpdateAsync(product);
        }
    }
}
