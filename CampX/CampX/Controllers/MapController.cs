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

        public IActionResult DisplayCampsites()
        {
            return Json(Service.DisplayCampsites());
        }

        [HttpGet]

        public IActionResult CampsiteDetails(int id)
        {
            var campsite = Service.CampsiteDetails(id);

            if (campsite == null)
            {
                return View("Error_NotFound");
            }

            return View("CampsiteDetails", campsite);
        }


        [HttpGet]

        public IActionResult Delete(int id)
        {

            return DeleteCampsite(id);
        }
        [HttpPost]

        public IActionResult DeleteCampsite(int id)
        {
            Service.DeleteCampsite(id);

            return RedirectToAction("ShowMap", "Map");
        }

        [HttpGet]

        public IActionResult EditCampsite(int id)
        {
            var campsite = Service.CampsiteDetails(id);
            
            return View("EditCampsite", campsite);
        }

        [HttpPost]

        public IActionResult EditCampsite(AddCampsiteModel model, int id)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            Service.EditCampsite(model,id);

            return RedirectToAction("ShowMap", "Map");
        }
    }

  
}
