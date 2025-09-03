using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using System;
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

        public IEnumerable<Category> GetAll()
        {
            return _categories.Find(_ => true).ToList();
        }

        public Category? Get(string id)
        {
            return _categories.Find(p => p.Id == id).FirstOrDefault();
        }

        public Category Add(Category item)
        {

            if (item.Id == null)
            {
                item.Id = ObjectId.GenerateNewId().ToString();
            }
            _categories.InsertOne(item);
            return item;
        }

        public void Remove(string id)
        {
            _categories.DeleteOne(p => p.Id == id);
        }

        public bool Update(Category item)
        {
            var result = _categories.ReplaceOne(p => p.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}