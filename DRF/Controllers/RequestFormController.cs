using DRF.Models;
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
        private readonly IRequestsRepository requestsRepository;
        private readonly IRequestDonorsRepository requestDonorsRepository;
        private readonly IRequestPartnersRepository partnersRepository;
        private readonly IRequestTargetSectorsRepository targetSectorsRepository;
        public RequestFormController(ILookupsRepository lookupsRepository, IOrganizationRepository organizationRepository, IRequestsRepository requestsRepository, IRequestDonorsRepository requestDonorsRepository, IRequestPartnersRepository partnersRepository, IRequestTargetSectorsRepository targetSectorsRepository)
        {
            this.lookupsRepository = lookupsRepository;
            this.organizationRepository = organizationRepository;
            this.requestsRepository = requestsRepository;
            this.requestDonorsRepository = requestDonorsRepository;
            this.partnersRepository = partnersRepository;
            this.targetSectorsRepository = targetSectorsRepository;
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
                OrganizationId = 1,
                CounterPartOrganizationList = organizationRepository.GetCounterPartOrganization(),
                IsReadOnly = false
            });
        }
        [HttpPost]
        public IActionResult OnCreatePost([FromBody] OnCreatePost model)
        {
            if (ModelState.IsValid)
            {
                var newReq = new Requests()
                {
                    BriefOnProgram = model.BriefOnProgram,
                    ContactPerson = model.ContactPerson,
                    CounterPart = model.CounterPart.Value,
                    CreatedAt = Helper.Today,
                    CreatedBy = 1,
                    Criteria = model.Criteria,
                    CurrentStatus = "N",
                    HiredSelfEmployed = model.HiredSelfEmployed == 1 ? true : false,
                    ProgramTitle = model.ProgramTitle,
                    ProjectEndDate = model.ProjectEndDate,
                    ProjectStartDate = model.ProjectStartDate,
                    ReferralDeliveryDL = model.ReferralDeliveryDL,
                    ReferralTotal = model.ReferralTotal,
                    TargetRequest = model.TargetRequest.Value,
                    TotalTarget = model.TotalTarget.Value,
                    ThirdPartyOrganization = 1,

                };


                if (requestsRepository.Create(newReq) > 0)
                {
                    List<RequestDonors> donors = new List<RequestDonors>();
                    foreach (var d in model.Donors)
                    {
                        donors.Add(new RequestDonors() { DonorId = d, RequestId = newReq.Id, CreatedAt = Helper.Today, CreatedBy = 1 });
                    }
                    requestDonorsRepository.Create(donors);
                    
                    List<RequestPartners> partners = new List<RequestPartners>();
                    foreach (var d in model.Partners)
                    {
                        partners.Add(new RequestPartners() { PartnerId = d, RequestId = newReq.Id, CreatedAt = Helper.Today, CreatedBy = 1 });
                    }
                    partnersRepository.Create(partners); 
                    
                    List<RequestTargetSectors> sectors = new List<RequestTargetSectors>();
                    foreach (var d in model.TargetSectors)
                    {
                        sectors.Add(new RequestTargetSectors() { TargetSectorsID = d, RequestId = newReq.Id, CreatedAt = Helper.Today, CreatedBy = 1 });
                    }
                    targetSectorsRepository.Create(sectors);

                   

                    return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.OK, "created successfully", null));

                }
                return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.BadRequest, "unable to create this request", null));
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

