using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductStore.Models
{
    public class Category
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
    }
}