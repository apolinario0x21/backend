using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _products.Find(_ => true).ToListAsync();
        }

        public async Task<Product?> GetAsync(string id)
        {
            return await _products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Product> CreateProductAsync(Product item)
        {
            await _products.InsertOneAsync(item);
            return item;
        }

        public async Task RemoveAsync(string id)
        {
            await _products.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<bool> UpdateAsync(Product item)
        {
            var result = await _products.ReplaceOneAsync(p => p.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}