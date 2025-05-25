using InventoryManager.Domain.Entities;
using InventoryManager.Domain.Interfaces;

namespace InventoryManager.Infrastructure.Repositories
{
    public class InMemoryProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetByIdAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return Task.FromResult(product);
        }

        public Task AddAsync(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Product product)
        {
            var index = _products.FindIndex(p => p.Id == product.Id);
            if (index >= 0)
                _products[index] = product;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product != null)
                _products.Remove(product);

            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetLowStockAsync()
        {
            var lowStock = _products.Where(p => p.Quantity < p.ReorderLevel);
            return Task.FromResult(lowStock.AsEnumerable());
        }
    }
}
