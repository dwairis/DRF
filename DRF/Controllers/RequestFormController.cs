﻿using DRF.Models;
using DRF.Repositories;
using DRF.Utilities;
using DRF.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace DRF.Controllers

{
    public class RequestFormController : Controller
    {
        private readonly ILookupsRepository lookupsRepository;
        private readonly IOrganizationRepository organizationRepository;
        public RequestFormController(ILookupsRepository lookupsRepository, IOrganizationRepository organizationRepository)
        {
            this.lookupsRepository = lookupsRepository;
            this.organizationRepository = organizationRepository;

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
                OrganizationList = organizationRepository.GetOrganizationAsDropDown(),
                OrganizationId = 1
            });
        }
        [HttpPost]
        public IActionResult OnCreatePost([FromBody] OnCreatePost model)
        {
            if (ModelState.IsValid)
            {
                return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.OK,"created sucxcc", null));
            }
            return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.BadRequest, Helper.RenderModelStateErrors(ModelState.Values.ToList()), "Invalid or missing fields!"));
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

