using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ProductStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("Products");
        }

        public IEnumerable<Product> GetAll()
        {
            return _products.Find(_ => true).ToList();
        }

        public Product? Get(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return null;
            }
            return _products.Find(p => p.Id == objectId).FirstOrDefault();
        }

        public Product Add(Product item)
        {
            _products.InsertOne(item);
            return item;
        }

        public void Remove(string id)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return;
            }
            _products.DeleteOne(p => p.Id == objectId);
        }

        public bool Update(Product item)
        {
            var result = _products.ReplaceOne(p => p.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}