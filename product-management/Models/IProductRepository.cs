namespace ProductStore.Models
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product? Get(string id);
        Product Add(Product item);
        void Remove(string id);
        bool Update(Product item);
    }
}