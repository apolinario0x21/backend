using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

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

        public Category? Get(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return null;
            }
            return _categories.Find(p => p.Id == objectId).FirstOrDefault();
        }

        public Category Add(Category item)
        {
            _categories.InsertOne(item);
            return item;
        }

        public void Remove(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return;
            }
            _categories.DeleteOne(p => p.Id == objectId);
        }

        public bool Update(Category item)
        {
            var result = _categories.ReplaceOne(p => p.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}