using DRF.Models;
using DRF.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DRF.Controllers

{
    public class RequestFormController : Controller
    {
        private readonly ILookupsRepository lookupsRepository;
        public RequestFormController(ILookupsRepository lookupsRepository)
        {
            this.lookupsRepository = lookupsRepository;
        }
        public IActionResult Index()
        {
            lookupsRepository.GetAll(); 
            var x = lookupsRepository.GetById(1);
            x.Value = "kkk";
            lookupsRepository.Update(x);

            lookupsRepository.Delete(x);


lookupsRepository.Create(new Lookups()
{
    CategoryID = 1,
})

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

