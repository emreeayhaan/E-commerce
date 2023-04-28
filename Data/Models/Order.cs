using Data.Dto;
using Data.Enum;
using MongoDB.Bson.Serialization.Attributes;
namespace Data.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public List<ProductDtoForShop>? ProductList { get; set; } = new List<ProductDtoForShop>();
        public string? CustomerId { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = 0;
    }
}
