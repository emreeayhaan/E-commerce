using Business.Business;
using ÇobanSteakHouse.Validation;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ÇobanSteakHouse.Controllers
{
    public class ProductController : Controller
    {
        ProductBusiness _productBusiness;
        CartBusiness _cartBusiness;

        public ProductController()
        {
            _productBusiness = new ProductBusiness();
            _cartBusiness = new CartBusiness();
        }

        public IActionResult Index()
        {
            return View(_productBusiness.Get());
        }

        [HttpGet, ActionName("Add")]
        public IActionResult PageContentForAdd()
        {
            return View();
        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductToDatabase(ProductDtoForAdd product)
        {
            ProductValidator.ValidateProductForAdd(_productBusiness, product, ModelState);

            if (ModelState.IsValid)
            {
                _productBusiness.Add(product);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet, ActionName("Update")]
        public IActionResult PageContentForUpdate(string id)
        {
            return View(_productBusiness.GetDtoForUpdate(id));
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateToDatabase(ProductDtoForUpdate product)
        {
            _productBusiness.Update(product);
            _cartBusiness.Delete(product.ProductId);
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("Delete")]
        public IActionResult PageContentForDelete(string id)
        {
            return View(_productBusiness.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromDatabaseWithProductId(string id)
        {
            _productBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
