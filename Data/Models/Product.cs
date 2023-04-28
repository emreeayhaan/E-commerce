using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductStock { get; set; }
        public double ProductPrice { get; set; }
        public string? ProductImageBase64 { get; set; }
        public int CategoryId { get; set; }
        public DateTime? LastUpdatedTime { get; set; } = DateTime.Now;
    }
}
