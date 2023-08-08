using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Trips;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Code.Base;
using Microsoft.AspNetCore.Mvc;

namespace CampX.Controllers
{
    public class TripController : BaseController
    {

        private readonly TripService Service;
        private readonly CampsiteService campsiteService;

        public TripController(ControllerDependencies dependencies, TripService service)
           : base(dependencies)
        {
            this.Service = service;
        }
        [HttpGet]
        public IActionResult ShowMap()
        {
            return View();
        }

        [HttpGet]

        public IActionResult DisplayCampsites()
        {
            return Json(Service.DisplayCampsites());
        }

        [HttpPost]

        public IActionResult AddTrip(AddTripModel model)
        {

            Service.AddTrip(model);
            return RedirectToAction("ShowMap", "Trip");
        }
    }
}
