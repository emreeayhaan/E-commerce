using Data.Dto;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Models
{
    public class Cart
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public List<ProductDtoForShop>? ProductList { get; set; } = new List<ProductDtoForShop>();
        public string? CustomerId { get; set; }

    }
}
