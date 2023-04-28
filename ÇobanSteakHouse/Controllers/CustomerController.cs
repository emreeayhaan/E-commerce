using Business.Business;
using ÇobanSteakHouse.Validation;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ÇobanSteakHouse.Controllers
{
    public class CustomerController : Controller
    {
        CustomerBusiness _customerBusiness;
        public CustomerController()
        {
            _customerBusiness = new CustomerBusiness();
        }

        public IActionResult Index()
        {
            return View(_customerBusiness.Get());
        }

        [HttpGet, ActionName("Add")]
        public IActionResult PageContentForAdd()
        {
            return View();
        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddCustomerToDatabase(CustomerDto customer)
        {
            CustomerValidator.ValidateCustomerForAdd(_customerBusiness, customer, ModelState);

            if (ModelState.IsValid)
            {
                _customerBusiness.Add(customer);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet, ActionName("Update")]
        public IActionResult PageContentforUpdate(string id)
        {
            return View(_customerBusiness.GetDto(id));
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateToDatase(CustomerDto customer)
        {
            _customerBusiness.Update(customer);
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("Delete")]
        public IActionResult PageContentForDelete(string id)
        {
            return View(_customerBusiness.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromDatabase(string id)
        {
            _customerBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
