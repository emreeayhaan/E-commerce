using Microsoft.AspNetCore.Http;

namespace Data.Dto
{
    public class ProductDtoForAdd
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductStock { get; set; }
        public string? ProductPrice { get; set; }
        public IFormFile? ProductFile { get; set; }
        public int CategoryId { get; set; }
    }
}
