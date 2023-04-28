using Data.Dto;
using Data.Models;
using MongoDB;

namespace Business.Business
{
    public class ProductBusiness
    {
        private MongoDBService<Product> _productService;

        public ProductBusiness()
        {
            _productService = new MongoDBService<Product>();
        }

        public List<Product> Get()
        {
            return _productService.GetAll();
        }

        public Product Get(string id)
        {
            return _productService.Get(x => x.Id == id);
        }

        public Product Get(int productId)
        {
            return _productService.Get(x => x.ProductId == productId);
        }

        public ProductDtoForUpdate GetDtoForUpdate(string id)
        {
            Product product = _productService.Get(x => x.Id == id);
            return new ProductDtoForUpdate()
            {
                ProductId = product.ProductId,
                ProductImageBase4 = product.ProductImageBase64,
                ProductStock = product.ProductStock,
                ProductDescription = product.ProductDescription,
                ProductName = product.ProductName,
                CategoryId = product.CategoryId,
                ProductPrice = product.ProductPrice.ToString(),
                LastUpdatedTime = product.LastUpdatedTime
            };
        }

        public List<ProductDtoForShop> GetDtoForShop()
        {
            List<ProductDtoForShop> productDtoList = new List<ProductDtoForShop>();
            foreach (Product product in _productService.GetAll())
            {
                productDtoList.Add(new ProductDtoForShop()
                {
                    CategoryId = product.CategoryId,
                    ProductImageBase64 = product.ProductImageBase64,
                    ProductName = product.ProductName,
                    ProductPrice = product.ProductPrice,
                    ProductStock = product.ProductStock,
                    ProductId = product.ProductId,
                    ProductDescription = product.ProductDescription
                });
            }
            return productDtoList;
        }

        public ProductDtoForShop GetDtoForShop(int productId)
        {
            Product product = Get(productId);
            return new ProductDtoForShop()
            {
                ProductId = productId,
                CategoryId = product.CategoryId,
                ProductImageBase64 = product.ProductImageBase64,
                ProductName = product.ProductName,
                ProductPrice = product.ProductPrice,
                ProductStock = product.ProductStock,
                ProductDescription = product.ProductDescription
            };
        }

        public void Add(ProductDtoForAdd productDto)
        {
            Product product = new Product()
            {
                ProductId = productDto.ProductId,
                ProductName = productDto.ProductName,
                ProductDescription = productDto.ProductDescription,
                ProductStock = productDto.ProductStock,
                CategoryId = productDto.CategoryId,
                ProductPrice = double.Parse(productDto.ProductPrice.Replace('.', ','))
            };
            using (var ms = new MemoryStream())
            {
                productDto.ProductFile?.CopyTo(ms);
                product.ProductImageBase64 = Convert.ToBase64String(ms.ToArray());
            }
            _productService.Add(product);
        }

        public void Update(ProductDtoForUpdate productDto)
        {
            Product product = Get(productDto.ProductId);
            product.ProductDescription = productDto.ProductDescription;
            product.ProductId = productDto.ProductId;
            product.ProductName = productDto.ProductName;
            product.CategoryId = productDto.CategoryId;
            product.ProductStock = productDto.ProductStock;
            product.ProductPrice = double.Parse(productDto.ProductPrice.Replace('.', ','));
            product.LastUpdatedTime = DateTime.Now;

            if (productDto.ProductFile == null)
                product.ProductImageBase64 = Get(productDto.ProductId).ProductImageBase64;
            else
                using (var ms = new MemoryStream())
                {
                    productDto.ProductFile.CopyTo(ms);
                    product.ProductImageBase64 = Convert.ToBase64String(ms.ToArray());
                }
            _productService.Update(x => x.Id == product.Id, product);
        }

        public void Delete(string id)
        {
            _productService.Delete(x => x.Id == id);
        }
    }

}
