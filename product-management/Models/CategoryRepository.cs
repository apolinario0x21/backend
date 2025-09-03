using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace ProductStore.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IMongoCollection<Category> _categories;

        public CategoryRepository(IMongoDatabase database)
        {
            _categories = database.GetCollection<Category>("Categories");
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.Find(_ => true).ToList();
        }

        public Category? Get(int id)
        {
            return _categories.Find(p => p.Id == id).FirstOrDefault();
        }

        public Category Add(Category item)
        {
            _categories.InsertOne(item);
            return item;
        }

        public void Remove(int id)
        {
            _categories.DeleteOne(c => c.Id == id);
        }

        public bool Update(Category item)
        {
            var result = _categories.ReplaceOne(c => c.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}