using Microsoft.AspNetCore.Mvc;
using DRF.ViewModels;
using DRF.Repositories;

namespace DRF.Controllers
{
    public class RequestsController : Controller
    {
        private readonly IRequestsRepository requestsRepository;

        public RequestsController(IRequestsRepository requestsRepository)
        {
            this.requestsRepository = requestsRepository;
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
    }
}
