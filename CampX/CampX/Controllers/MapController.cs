using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.Code.Base;
using CampX.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using CampX.BusinessLogic.Implementations.Images;

namespace CampX.Controllers
{
    public class MapController : BaseController
    {

        private readonly CampsiteService Service;
        private readonly ImagesService imgService;

        public MapController(ControllerDependencies dependencies, CampsiteService service,ImagesService imgService)
           : base(dependencies)
        {
            this.Service = service;
            this.imgService = imgService;

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
            /*for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                if (file != null)
                {
                    // Do something here
                }
            }*/
                if (model == null)
            {
                return View("Error_NotFound");
            }
            var imgList = imgService.AddImages(model.Images);
            
            Service.AddCampsite(model,imgList);

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
            var campsite = Service.CampsiteToEdit(id);
            
            return View("EditCampsite", campsite);
        }

        [HttpGet]

        public IActionResult EditCampsiteJson(int id)
        {
            return Json(Service.CampsiteToEdit(id));          
        }

        [HttpPost]

        public IActionResult EditCampsite(EditCampsiteModel model, int id)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            var imgList = imgService.AddImages(model.Images);

            Service.EditCampsite(model,imgList,id);

            return RedirectToAction("ShowMap", "Map");
        }
    }

  
}
