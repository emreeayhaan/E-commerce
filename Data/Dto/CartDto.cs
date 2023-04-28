namespace Data.Dto
{
    public class CartDto
    {
        public List<ProductDtoForShop>? ProductList { get; set; }
        public string? CustomerId { get; set; }
    }
}