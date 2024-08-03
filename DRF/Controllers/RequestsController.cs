using Microsoft.AspNetCore.Mvc;
using DRF.ViewModels;
using DRF.Repositories;

namespace DRF.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestsRepository requestsRepository;
        private readonly IRequestUpdatesRepository requestUpdatesRepository;

        public RequestsController(IRequestsRepository requestsRepository, IRequestUpdatesRepository requestUpdatesRepository)
        {
            this.requestsRepository = requestsRepository;
            this.requestUpdatesRepository = requestUpdatesRepository;
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

        public IActionResult Details(int id)
        {
            var request = requestsRepository.GetRequestDetailsById(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [HttpGet]
        public IActionResult GetRequestDetails(int id)
        {
            var requestDetails = requestsRepository.GetRequestDetailsById(id);
            return Json(requestDetails);
        }

        public IActionResult GetRequestUpdates(int requestId)
        {
            var updates = requestUpdatesRepository.GetRequestUpdates(requestId);

            if (updates == null || !updates.Any())
            {
                return NotFound(new { message = "No updates found for this request." });
            }

            return Json(updates);
        }
    }
}