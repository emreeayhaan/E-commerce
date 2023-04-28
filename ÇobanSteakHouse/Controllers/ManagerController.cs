using Business.Business;
using ÇobanSteakHouse.Validation;
using Data.Dto;
using Microsoft.AspNetCore.Mvc;

namespace ÇobanSteakHouse.Controllers
{
    public class ManagerController : Controller
    {
        ManagerBusiness _managerBusiness;
        public ManagerController()
        {
            _managerBusiness = new ManagerBusiness();
        }

        public IActionResult Index()
        {
            return View(_managerBusiness.Get());
        }

        [HttpGet, ActionName("Add")]
        public IActionResult PageContentForAdd()
        {
            return View();
        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public IActionResult AddManagerToDatabase(ManagerDto manager)
        {
            ManagerValidator.ValidateManagerForAdd(_managerBusiness, manager, ModelState);

            if (ModelState.IsValid)
            {
                _managerBusiness.Add(manager);
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpGet, ActionName("Update")]
        public IActionResult PageContentforUpdate(string id)
        {
            return View(_managerBusiness.GetDto(id));
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateToDatase(ManagerDto manager)
        {
            _managerBusiness.Update(manager);
            return RedirectToAction("Index");
        }

        [HttpGet, ActionName("Delete")]
        public IActionResult PageContentForDelete(string id)
        {
            return View(_managerBusiness.Get(id));
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteFromDatabase(string id)
        {
            _managerBusiness.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
