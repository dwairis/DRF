using DRF.Models;
using Microsoft.AspNetCore.Mvc;

namespace DRF.Controllers

{
    public class RequestFormController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Submit(RequestFormModel model)
        {
            if (ModelState.IsValid)
            {
                // Handle the form submission here
                return RedirectToAction("Success");
            }
            return View("Index");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}

