using Data.Dto;
using Data.Models;
using MongoDB;

namespace Business.Business
{
    public class CartBusiness
    {
        MongoDBService<Cart> _cartService;

        public CartBusiness()
        {
            _cartService = new MongoDBService<Cart>();
        }

        public List<Cart> Get()
        {
            return _cartService.GetAll();
        }

        public Cart Get(string id)
        {
            return _cartService.Get(x => x.Id == id);
        }

        public Cart Get(string customerId, bool isCustomerId)
        {
            return _cartService.Get(x => x.CustomerId == customerId);
        }

        public CartDto GetDto(string customerId)
        {
            Cart cart = Get(customerId, true);
            if (cart == null)
                return null;
            return new CartDto()
            {
                CustomerId = cart.CustomerId,
                ProductList = cart.ProductList
            };
        }

        public void Add(ProductDtoForShop product, string customerId)
        {
            Cart cart = Get(customerId, true);
            if (cart == null)
            {
                cart = new Cart();
                cart.CustomerId = customerId;
                product.ProductStock = 1;
                cart.ProductList.Add(product);
                _cartService.Add(cart);
            }
            else
            {
                var oldProduct = cart.ProductList.FirstOrDefault(x => x.ProductId == product.ProductId);
                if (oldProduct == null)
                {
                    product.ProductStock = 1;
                    cart.ProductList.Add(product);
                    _cartService.Update(x => x.CustomerId == cart.CustomerId, cart);
                }
                else if (product.ProductStock > cart.ProductList.FirstOrDefault(x => x.ProductId == product.ProductId).ProductStock)
                {
                    cart.ProductList.Where(x => x.ProductId == product.ProductId).FirstOrDefault().ProductStock += 1;
                    _cartService.Update(x => x.CustomerId == cart.CustomerId, cart);
                }
            }
        }

        public void Update(ProductDtoForShop product, string customerId)
        {
            Cart cart = Get(customerId, true);
            cart.ProductList.Add(product);
            _cartService.Update(x => x.CustomerId == cart.CustomerId, cart);
        }

        public void Delete(int productId, string customerId)
        {
            Cart cart = Get(customerId, true);
            Delete(cart, productId);
        }

        public void Delete(int productId)
        {
            IEnumerable<Cart> cartList2 = _cartService.GetAll().Where(x => x.ProductList.Any(y => y.ProductId == productId));

            foreach (Cart cart in cartList2)
            {
                Delete(cart, productId);
            }
        }

        private void Delete(Cart cart, int productId)
        {
            cart.ProductList.Remove(cart.ProductList.Find(x => x.ProductId == productId));
            _cartService.Update(x => x.CustomerId == cart.CustomerId, cart);

            if (cart.ProductList.Count == 0)
                _cartService.Delete(x => x.CustomerId == cart.CustomerId);
        }
    }
}
