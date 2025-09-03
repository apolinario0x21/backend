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
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            // O MongoDB gera o ObjectId automaticamente
            _categories.InsertOne(item);
            return item;
        }

        public void Remove(int id)
        {
            _categories.DeleteOne(p => p.Id == id);
        }

        public bool Update(Category item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            var result = _categories.ReplaceOne(p => p.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}