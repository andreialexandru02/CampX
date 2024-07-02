using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Map.Models;
using CampX.Code.Base;
using CampX.Entities;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using CampX.BusinessLogic.Implementations.Images;
using Microsoft.AspNetCore.Authorization;


namespace CampX.Controllers
{
    [Authorize]
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
        [AllowAnonymous]
        public IActionResult ShowMap()
        {
            var model = Service.GetCampsites();
            return View(model);
        }
        [HttpGet]
        public IActionResult AddCampsite()
        {
            var model = new AddCampsiteModel();

            /*string originalUrl = Request.Url.AbsoluteUri;

            // Store the original URL in a session or cookie
            Session["OriginalUrl"] = originalUrl;*/
            
            return View("AddCampsite", model);
        }

        [HttpPost]
        //[Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult AddCampsite(AddCampsiteModel model)
        {
                if (model == null)
            {
                return View("Error_NotFound");
            }
            var imgList = new List<int>();
            if (model.Images != null)
            {
                imgList = imgService.AddImages(model.Images);
            }
            
            Service.AddCampsite(model,imgList);

            return RedirectToAction("ShowMap", "Map");
        }

        [HttpGet]
        [AllowAnonymous]
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
            if (!Service.IdExists(id))

            {
                return View("Error_NotFound");
            }
            if (!Service.CheckCampsiteOwner(id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            return DeleteCampsite(id);
        }
        [HttpPost]

        public IActionResult DeleteCampsite(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            if (!Service.CheckCampsiteOwner(id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.DeleteCampsite(id);

            return RedirectToAction("ShowMap", "Map");
        }

        [HttpGet]

        public IActionResult EditCampsite(int id)
        {

            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            if (!Service.CheckCampsiteOwner(id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            var campsite = Service.CampsiteToEdit(id);
            
            return View("EditCampsite", campsite);
        }

        [HttpGet]

        public IActionResult EditCampsiteJson(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            return Json(Service.CampsiteToEdit(id));          
        }

        [HttpPost]

        public IActionResult EditCampsite(EditCampsiteModel model, int id)
        {
           
            if (model == null)
            {
                return View("Error_NotFound");
            }
            if (model.ImageIds != null)
            {
                imgService.DeleteImages(model.ImageIds);
            }
            var imgList = new List<int>();
            if (model.Images != null)
            {
                imgList = imgService.AddImages(model.Images);
            }
            Service.EditCampsite(model,imgList,id);

            // return RedirectToAction("ShowMap", "Map");
            return RedirectToAction("CampsiteDetails", "Map", new { id = id });
        }
    }

  
}
