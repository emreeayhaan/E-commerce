using Business.Business;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ÇobanSteakHouse.Controllers
{
    public class OrderController : Controller
    {
        private OrderBusiness _orderBusiness;
        public OrderController()
        {
            _orderBusiness = new OrderBusiness();
        }

        public IActionResult Index()
        {
            return View(_orderBusiness.GetDto());
        }

        [HttpGet, ActionName("Edit")]
        public IActionResult PageContentForEdit(string id)
        {
            return View(_orderBusiness.GetDto(id, true));
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult UpdateOrderStatus(OrderDto order)
        {
            _orderBusiness.Update(order.OrderId, order.OrderStatus);
            return RedirectToAction("Index");
        }
    }
}
