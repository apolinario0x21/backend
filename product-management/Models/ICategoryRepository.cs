using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Models
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetAsync(string id);
        Task<Category> AddAsync(Category item);
        Task RemoveAsync(string id);
        Task<bool> UpdateAsync(Category item);
    }
}