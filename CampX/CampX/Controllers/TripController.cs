using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Trips;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Code.Base;
using Microsoft.AspNetCore.Http.HttpResults;
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

        [HttpGet]
        public IActionResult ShowTrips()
        {
            var models = Service.ShowTrips();

            return View(models);
        }
        [HttpGet]
        public IActionResult TripDetails(int id)
        {
            var model = Service.TripDetails(id);
            return View("TripDetails", model);
        }
        [HttpGet]
        public IActionResult TripDetailsJSON([FromQuery]int id)
        {
            return Json(Service.TripDetails(id));
        }
        
        [HttpGet]
        public IActionResult DeleteTrip(int id)
        {
            Service.DeleteTrip(id);
            return RedirectToAction("ShowTrips", "Trip");
        }
        [HttpPost]
        public IActionResult SearchTrip(string code)
        {
            var id = Service.SearchCode(code);
            if(id == -1)
            {
                return NotFound();
            }
            // return Ok(new { redirectUrl = $"/Trip/TripDetails/{id}" });
            //return RedirectToAction("ShowMap", "Trip");
            return Json(id);
        }

        [HttpGet]

        public IActionResult ShowCurrentCamperTrips()
        {
            var model = Service.ShowCurrentCamperTrips(); 
            return View("ShowCurrentCamperTrips", model);
        }
    }
}
