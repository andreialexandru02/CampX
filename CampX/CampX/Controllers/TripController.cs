using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Trips;
using CampX.BusinessLogic.Implementations.Trips.Models;
using CampX.Code.Base;
using CampX.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace CampX.Controllers
{
    [Authorize]
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

            return View( new AddTripModel
            {
                Date = DateTime.Now,
            });
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
        [AllowAnonymous]
        public IActionResult ShowTrips()
        {
            var models = Service.ShowTrips();

            return View(models);
        }
        [HttpGet]

        public IActionResult TripDetails(int id, [FromQuery]string code = null)
        {
            var model = Service.TripDetails(id);
            if (model == null)
            {
                return View("Error_NotFound");
            }
            if(!model.IsPublic && model.Code != code)
            {
                return RedirectToAction("Error_Unauthorized", "Home");

            }
            return View("TripDetails", model);
        }
        [HttpGet]
        public IActionResult TripDetailsJSON([FromQuery] int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            return Json(Service.TripDetails(id));
        }

        [HttpPost]
        public IActionResult DeleteTrip(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            if (!Service.CheckOrganizer(id) && !Service.isAdmin())
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.DeleteTrip(id);
            return RedirectToAction("ShowTrips", "Trip");
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult SearchTrip(string code)
        {
            var id = Service.SearchCode(code);
            if (id == -1)
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

        [HttpGet]
        public IActionResult EditTrip(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            if (!Service.CheckOrganizer(id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            var model = Service.TripToEdit(id);
            
            return View("EditTrip", model);
        }
        [HttpPost]
        public IActionResult EditTrip(ShowTripsModel model)
        {
            if(model == null)
            {
                return View("Error_NotFound");
            }

            Service.EditTrip(model);
            return Ok(model.Id);
        }


        [HttpGet]

        public IActionResult FinishTrip()
        {
            var models = Service.GetFinishedTrips();

            return View(models);
        }

        [HttpPost]

        public IActionResult FinishTrip(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            if (!Service.CheckOrganizer(id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.FinishTrip(id);

            return RedirectToAction("FinishTrip");
            
        }
    }
}
