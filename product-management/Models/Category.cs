using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductStore.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public required string Name { get; set; }
    }
}