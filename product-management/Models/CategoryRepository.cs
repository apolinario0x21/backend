using System.Collections.Generic;
using System.Linq;

namespace ProductStore.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly List<Category> _categories = new();
        private int _nextId = 1;

        public CategoryRepository()
        {
            Add(new Category { Name = "Categoria A" });
            Add(new Category { Name = "Categoria B" });
            Add(new Category { Name = "Categoria C" });
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories;
        }

        public Category? Get(int id)
        {
            return _categories.Find(p => p.Id == id);
        }

        public Category Add(Category item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            item.Id = _nextId++;
            _categories.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            _categories.RemoveAll(p => p.Id == id);
        }

        public bool Update(Category item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            int index = _categories.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            _categories.RemoveAt(index);
            _categories.Add(item);
            return true;
        }
    }
}