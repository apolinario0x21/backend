using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace ProductStore.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>("Categories");
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categories.Find(_ => true).ToListAsync();
        }

        public async Task<Category?> GetAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
            return await _categories.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Category> AddAsync(Category item)
        {
            await _categories.InsertOneAsync(item);
            return item;
        }

        public async Task RemoveAsync(string id)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, id);
            await _categories.DeleteOneAsync(filter);
        }

        public async Task<bool> UpdateAsync(Category item)
        {
            var filter = Builders<Category>.Filter.Eq(c => c.Id, item.Id);
            var result = await _categories.ReplaceOneAsync(filter, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}