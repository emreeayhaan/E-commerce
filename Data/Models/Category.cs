using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
