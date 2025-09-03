using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductStore.Models
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        
        Task<Product?> GetAsync(string id);
        
        Task<Product> CreateProductAsync(Product item);

        Task RemoveAsync(string id);
        
        Task<bool> UpdateAsync(Product item);
    }
}