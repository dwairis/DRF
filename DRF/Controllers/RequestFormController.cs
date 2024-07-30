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
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult OnCreateInit()
        {
            return Json(new
            {
                DonorsList = lookupsRepository.GetByCategory(Utilities.LookupsCategoryEnum.DONORS),
                PartnersList = lookupsRepository.GetByCategory(Utilities.LookupsCategoryEnum.PARTNERS),
                TargetSectorsList = lookupsRepository.GetByCategory(Utilities.LookupsCategoryEnum.TARGET_SECTORS),
            });
        }

        //[HttpPost]
        //public IActionResult Submit(RequestFormModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        // Handle the form submission here
        //        return RedirectToAction("Success");
        //    }
        //    return View("Index");
        //}

        public IActionResult Success()
        {
            return View();
        }
    }
}

