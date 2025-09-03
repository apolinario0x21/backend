using System.Collections.Generic;

namespace ProductStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAll();
        Category? Get(string id);
        Category Add(Category item);
        void Remove(string id);
        bool Update(Category item);
    }
}