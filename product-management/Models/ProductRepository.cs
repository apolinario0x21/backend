using System.Collections.Generic;
using System.Linq;

namespace ProductStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> _products = new();
        private int _nextId = 1;

        public ProductRepository()
        {
           
            Add(new Product { Name = "Product A", Description = "product description", Category = "Product", Price = 100.00M, QuantityInStock = 10 });
            Add(new Product { Name = "Product B", Description = "product description", Category = "Product", Price = 100.00M, QuantityInStock = 5 });
            Add(new Product { Name = "Product C", Description = "product description", Category = "Product", Price = 100.00M, QuantityInStock = 15 });

            
        }

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? Get(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            item.Id = _nextId++;
            _products.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            _products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            int index = _products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            _products.RemoveAt(index);
            _products.Add(item);
            return true;
        }
    }
}