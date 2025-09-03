using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductStore.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Category { get; set; }
        public int QuantityInStock { get; set; }
        
    }
}
