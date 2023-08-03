using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.Code.Base;
using CampX.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CampX.Controllers
{
    public class MapController : BaseController
    {

        private readonly CampsiteService Service;

        public MapController(ControllerDependencies dependencies, CampsiteService service)
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
        public IActionResult AddCampsite()
        {
            var model = new AddCampsiteModel();

            return View("AddCampsite", model);
        }

        [HttpPost]

        public IActionResult AddCampsite(AddCampsiteModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            Service.AddCampsite(model);

            return RedirectToAction("ShowMap", "Map");
        }

        [HttpGet]

        public IActionResult GetCoordinates()
        {
            return Json(Service.GetCoordinates());
        }

    }

  
}
