namespace ProductStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? Get(int id);
        Category Add(Category item);
        void Remove(int id);
        bool Update(Category item);
    }
}