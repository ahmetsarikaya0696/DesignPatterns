using DecoratorDesignPattern.Models;
using Microsoft.Extensions.Caching.Memory;

namespace DecoratorDesignPattern.Repositories.Decorator
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private readonly IMemoryCache _memoryCache;

        private const string cacheKey = "products";
        public ProductRepositoryCacheDecorator(IProductRepository productRepository, IMemoryCache memoryCache) : base(productRepository)
        {
            _memoryCache = memoryCache;
        }

        public override async Task<List<Product>> GetAllAsync()
        {
            if (_memoryCache.TryGetValue(cacheKey, out List<Product> cachedProducts))
            {
                return cachedProducts;
            }

            await UpdateCacheAsync();

            return _memoryCache.Get<List<Product>>(cacheKey);
        }

        public override async Task<List<Product>> GetAllAsync(string userId)
        {
            var products = await GetAllAsync();

            return products.Where(x => x.UserId == userId).ToList(); 
        }

        public override async Task<Product> AddAsync(Product product)
        {
            await base.AddAsync(product);

            await UpdateCacheAsync();

            return product;
        }

        public override async Task UpdateAsync(Product product)
        {
            await base.UpdateAsync(product);

            await UpdateCacheAsync();
        }

        public override async Task RemoveAsync(Product product)
        {
            await base.RemoveAsync(product);

            await UpdateCacheAsync();
        }

        private async Task UpdateCacheAsync()
        {
            _memoryCache.Set(cacheKey, await base.GetAllAsync());
        }
    }
}
