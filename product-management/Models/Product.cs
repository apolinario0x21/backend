using System.ComponentModel.DataAnnotations;

namespace ProductStore.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Category { get; set; }
        public int QuantityInStock { get; set; }
        
    }
}
