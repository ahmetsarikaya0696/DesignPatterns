using DecoratorDesignPattern.Models;

namespace DecoratorDesignPattern.Repositories.Decorator
{
    public class ProductRepositoryLogDecorator : BaseProductRepositoryDecorator
    {
        private readonly ILogger<ProductRepositoryLogDecorator> _logger;

        public ProductRepositoryLogDecorator(IProductRepository productRepository, ILogger<ProductRepositoryLogDecorator> logger) : base(productRepository)
        {
            _logger = logger;
        }

        public override Task<List<Product>> GetAllAsync()
        {
            _logger.LogInformation("GetAllAsync() çalıştı");
            return base.GetAllAsync();
        }

        public override Task<List<Product>> GetAllAsync(string userId)
        {
            _logger.LogInformation("GetAllAsync(string userId) çalıştı");
            return base.GetAllAsync(userId);
        }

        public override Task<Product> AddAsync(Product product)
        {
            _logger.LogInformation("AddAsync(Product product) çalıştı");
            return base.AddAsync(product);
        }

        public override Task UpdateAsync(Product product)
        {
            _logger.LogInformation("UpdateAsync(Product product) çalıştı");
            return base.UpdateAsync(product);
        }

        public override Task RemoveAsync(Product product)
        {
            _logger.LogInformation("RemoveAsync(Product product) çalıştı");
            return base.RemoveAsync(product);
        }
    }
}
