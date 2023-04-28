using Business.Business;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ÇobanSteakHouse.Controllers
{
    public class ShopController : Controller
    {
        ProductBusiness _productBusiness;
        CategoryBusiness _categoryBusiness;
        CartBusiness _cartBusiness;
        OrderBusiness _orderBusiness;

        public ShopController()
        {
            _productBusiness = new ProductBusiness();
            _categoryBusiness = new CategoryBusiness();
            _cartBusiness = new CartBusiness();
            _orderBusiness = new OrderBusiness();
        }

        public IActionResult Index()
        {
            var tupleModel = new Tuple<List<CategoryDto>, List<ProductDtoForShop>>(_categoryBusiness.GetDto(), _productBusiness.GetDtoForShop());
            return View(tupleModel);
        }

        [HttpGet, ActionName("SingleProduct")]
        public IActionResult PageContentForSingleProduct(int id)
        {
            return View(_productBusiness.GetDtoForShop(id));
        }

        [HttpGet, ActionName("Cart")]
        public IActionResult PageContentForCart()
        {
            var cart = _cartBusiness.GetDto(HttpContext.Session.GetString("UserId"));
            if (cart == null)
                return RedirectToAction("Index");
            return View(cart);
        }

        [HttpGet, ActionName("AddToCart")]
        public IActionResult AddProductToCart(int id)
        {
            _cartBusiness.Add(_productBusiness.GetDtoForShop(id), HttpContext.Session.GetString("UserId"));
            return RedirectToAction("Cart");
        }
        [HttpGet, ActionName("DeleteFromDatabase")]
        public IActionResult DeleteProductFromDatabase(int id)
        {
            _cartBusiness.Delete(id, HttpContext.Session.GetString("UserId"));
            return RedirectToAction("Cart");
        }

        [HttpGet, ActionName("Buy")]
        public IActionResult BuyNow()
        {
            _orderBusiness.Add(HttpContext.Session.GetString("UserId"));
            return RedirectToAction("Order");
        }

        [HttpGet, ActionName("Order")]
        public IActionResult GetOrdersFromDatabase()
        {
            return View(_orderBusiness.GetDto(HttpContext.Session.GetString("UserId")));
        }
    }
}
