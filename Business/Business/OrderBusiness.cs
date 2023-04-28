using Data.Dto;
using Data.Enum;
using Data.Models;
using MongoDB;
namespace Business.Business
{
    public class OrderBusiness
    {
        MongoDBService<Order> _orderService;
        MongoDBService<Cart> _cartService;
        MongoDBService<Product> _productService;

        public OrderBusiness()
        {
            _orderService = new MongoDBService<Order>();
            _cartService = new MongoDBService<Cart>();
            _productService = new MongoDBService<Product>();
        }

        public List<Order> Get()
        {
            return _orderService.GetAll();
        }

        public Order Get(string id)
        {
            return _orderService.Get(x => x.Id == id);
        }

        public Order Get(string orderId, bool isOrderId)
        {
            return _orderService.Get(x => x.OrderId == orderId);
        }

        public List<OrderDto> GetDto()
        {
            List<OrderDto> orders = new List<OrderDto>();
            foreach (var item in _orderService.GetAll())
            {
                orders.Add(
                    new OrderDto()
                    {
                        CustomerId = item.CustomerId,
                        ProductList = item.ProductList,
                        OrderId = item.OrderId,
                        OrderStatus = item.OrderStatus,
                        OrderTime = item.OrderTime
                    }
                );
            }
            return orders;
        }

        public List<OrderDto> GetDto(string customerId)
        {
            List<OrderDto> orders = new List<OrderDto>();
            foreach (var item in _orderService.GetAll(x => x.CustomerId == customerId))
            {
                orders.Add(
                    new OrderDto()
                    {
                        CustomerId = item.CustomerId,
                        ProductList = item.ProductList,
                        OrderId = item.OrderId,
                        OrderStatus = item.OrderStatus,
                        OrderTime = item.OrderTime
                    }
                );
            }
            return orders;
        }

        public OrderDto GetDto(string orderId, bool isOrderId)
        {
            Order order = _orderService.Get(x => x.OrderId == orderId);
            return new OrderDto()
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId,
                OrderTime = order.OrderTime,
                ProductList = order.ProductList,
                OrderStatus = order.OrderStatus
            };
        }

        public void Add(string customerId)
        {
            Cart cart = _cartService.Get(x => x.CustomerId == customerId);
            _orderService.Add(new Order()
            {
                CustomerId = cart.CustomerId,
                ProductList = cart.ProductList,
                OrderId = Guid.NewGuid().ToString()
            });
            _cartService.Delete(x => x.CustomerId == customerId);
            foreach (var obj in cart.ProductList)
            {
                Product product = _productService.Get(x => x.ProductId == obj.ProductId);
                product.ProductStock -= obj.ProductStock;
                _productService.Update(x => x.ProductId == product.ProductId, product);
            }
        }

        public void Update(string orderId, OrderStatus orderStatus)
        {
            Order order = Get(orderId, true);
            order.OrderStatus = orderStatus;
            _orderService.Update(x => x.OrderId == order.OrderId, order);
        }

        public void Delete(string customerId)
        {
            _orderService.Delete(x => x.CustomerId == customerId);
        }

    }
}
