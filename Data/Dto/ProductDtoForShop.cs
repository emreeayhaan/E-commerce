namespace Data.Dto
{
    public class ProductDtoForShop
    {
        public int ProductStock { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? ProductImageBase64 { get; set; }
        public string? ProductDescription { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
    }
}
