using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

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

        public Product? Get(int id)
        {
            return _products.Find(p => p.Id == id).FirstOrDefault();
        }
        
        public Product Add(Product item)
        {
            _products.InsertOne(item);
            return item;
        }

        public void Remove(int id)
        {
            _products.DeleteOne(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            var result = _products.ReplaceOne(p => p.Id == item.Id, item);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }
    }
}