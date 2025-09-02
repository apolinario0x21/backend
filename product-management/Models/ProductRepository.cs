namespace ProductStore.Models
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();
        private int _nextId = 1;

        public ProductRepository()
        {
           
            Add(new Product { Name = "Product A", Description = "product description", Category = "Product", Price = 275.00M, Stock = 10 });
            Add(new Product { Name = "Product B", Description = "product description", Category = "Product", Price = 150.00M, Stock = 5 });
            Add(new Product { Name = "Product C", Description = "product description", Category = "Product", Price = 50.00M, Stock = 20 });

            
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product? Get(int id)
        {
            return products.Find(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            products.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            products.RemoveAll(p => p.Id == id);
        }

        public bool Update(Product item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = products.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            products.RemoveAt(index);
            products.Add(item);
            return true;
        }
    }
}