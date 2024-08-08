using Microsoft.AspNetCore.Mvc;
using DRF.ViewModels;
using DRF.Repositories;
using Newtonsoft.Json;
using DRF.Utilities;

namespace DRF.Controllers
{
    public class RequestsController : Controller
    {
        private readonly ILookupsRepository lookupsRepository;
        private readonly IOrganizationRepository organizationRepository;
        private readonly IRequestsRepository requestsRepository;
        private readonly IRequestDonorsRepository requestDonorsRepository;
        private readonly IRequestPartnersRepository partnersRepository;
        private readonly IRequestTargetSectorsRepository targetSectorsRepository;
        private readonly IRequestStatusRepository statusRepository;
        public RequestsController(IRequestStatusRepository statusRepository, ILookupsRepository lookupsRepository, IOrganizationRepository organizationRepository, IRequestsRepository requestsRepository, IRequestDonorsRepository requestDonorsRepository, IRequestPartnersRepository partnersRepository, IRequestTargetSectorsRepository targetSectorsRepository)
        {
            this.lookupsRepository = lookupsRepository;
            this.organizationRepository = organizationRepository;
            this.requestsRepository = requestsRepository;
            this.requestDonorsRepository = requestDonorsRepository;
            this.partnersRepository = partnersRepository;
            this.targetSectorsRepository = targetSectorsRepository;
            this.statusRepository = statusRepository;
        }

        [HttpGet]
        public IActionResult RequestsList()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRequests()
        {
            var requests = requestsRepository.GetAllRequests();
            return Json(requests);
        }

        //public IActionResult Details(int id)
        //{
        //    var request = requestsRepository.GetRequestDetailsById(id);

        //    if (request == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(id);
        //}

        [HttpGet]
        public IActionResult GetRequestDetails(int id)
        {
            var requestDetails = requestsRepository.GetRequestDetailsById(id);
            return Json(requestDetails);
        }

        [HttpGet]
        public IActionResult GetRequestStatus(int id)
        {
            var updates = statusRepository.GetRequestStatus(id).ToList();

            if (updates == null || !updates.Any())
            {
                return Json(new { success = false, message = "No status found for this request." });
            }

            return Json(updates);
        }

        [HttpPost]
        public IActionResult OnUpdatePost([FromBody] OnCreatePost model)
        {
            if (ModelState.IsValid && model.Id > 0)
            {
                var current = requestsRepository.GetById(model.Id);

                if (current.CurrentStatus == "R")
                {
                    current.BriefOnProgram = model.BriefOnProgram;
                    current.ContactPerson = model.ContactPerson;
                    current.CounterPart = model.CounterPart.Value;
                    current.CreatedAt = Helper.Today;
                    current.CreatedBy = 1;
                    current.Criteria = model.Criteria;
                    current.CurrentStatus = "N";
                    current.HiredSelfEmployed = model.HiredSelfEmployed == 1 ? true : false;
                    current.ProgramTitle = model.ProgramTitle;
                    current.ProjectEndDate = model.ProjectEndDate;
                    current.ProjectStartDate = model.ProjectStartDate;
                    current.ReferralDeliveryDL = model.ReferralDeliveryDL;
                    current.ReferralTotal = model.ReferralTotal;
                    current.TargetRequest = model.TargetRequest.Value;
                    current.TotalTarget = model.TotalTarget.Value;
                    current.ThirdPartyOrganization = 1;



                    if (requestsRepository.Update(current))
                    {
                        requestsRepository.DeleteRequestMultiValues(current.Id);

                        List<RequestDonors> donors = new List<RequestDonors>();
                        foreach (var d in model.Donors)
                        {
                            donors.Add(new RequestDonors() { DonorId = d, RequestId = current.Id, CreatedAt = Helper.Today, CreatedBy = 1 });
                        }
                        requestDonorsRepository.Create(donors);


                        //delete 
                        List<RequestPartners> partners = new List<RequestPartners>();
                        foreach (var d in model.Partners)
                        {
                            partners.Add(new RequestPartners() { PartnerId = d, RequestId = current.Id, CreatedAt = Helper.Today, CreatedBy = 1 });
                        }
                        partnersRepository.Create(partners);

                        List<RequestTargetSectors> sectors = new List<RequestTargetSectors>();
                        foreach (var d in model.TargetSectors)
                        {
                            sectors.Add(new RequestTargetSectors() { TargetSectorsID = d, RequestId = current.Id, CreatedAt = Helper.Today, CreatedBy = 1 });
                        }
                        targetSectorsRepository.Create(sectors);



                        return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.OK, "updated successfully", null));

                    }
                    return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.BadRequest, "unable to create this request", null));
                }
                else
                {
                    return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.BadRequest, "unable to create this request", null));

                }
            }
            return Json(new JsonResponseMessage<string>(System.Net.HttpStatusCode.BadRequest, Helper.RenderModelStateErrors(ModelState.Values.ToList()), "Invalid or missing fields!"));
        }
        #region Request Details

        public IActionResult Details(int id)
        {
            return View(id);
        }

        public IActionResult OnDetailsInit(int id)
        {
            var current = requestsRepository.GetRequestDetails(id);
            return Json(new
            {
                DonorsList = lookupsRepository.GetByCategory(Utilities.LookupsCategoryEnum.DONORS),
                PartnersList = lookupsRepository.GetByCategory(Utilities.LookupsCategoryEnum.PARTNERS),
                TargetSectorsList = lookupsRepository.GetByCategory(Utilities.LookupsCategoryEnum.TARGET_SECTORS),
                OrganizationList = organizationRepository.GetOrganizationAsDropDown(),
                OrganizationId = 1,
                CounterPartOrganizationList = organizationRepository.GetCounterPartOrganization(),
                data = current,
                IsReadOnly = current.CurrentStatus != "R"

            });
        }
        #endregion

    }
}