using Data.Enum;

namespace Data.Dto
{
    public class OrderDto
    {
        public string? OrderId { get; set; }
        public List<ProductDtoForShop>? ProductList { get; set; }
        public string? CustomerId { get; set; }
        public DateTime OrderTime { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = 0;
    }
}
