using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Reviews;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CampX.Controllers
{
    [Authorize]

    public class ReviewController : BaseController
    {

        private readonly ReviewService Service;

        public ReviewController(ControllerDependencies dependencies, ReviewService service)
           : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ShowReviews(int id)
        {
            return Json(Service.ShowReviews(id));
        }

        //[HttpGet]

        //public IActionResult Delete(int id,int idCampsite)
        //{               
        //    return DeleteReview(id, idCampsite);
        //}

        [HttpPost]
         
        public IActionResult DeleteReview(DeleteReviewModel model)
        {
            if (!Service.CheckReviewOwner(model.Id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.DeleteReview(model.Id);

            return RedirectToAction("CampsiteDetails", "Map", new { id = model.CampsiteId });

        }

        [HttpPost]

        public IActionResult AddReview(AddReviewModel model)
        {
            Service.AddReview(model);

            return RedirectToAction("CampsiteDetails", "Map", new { id = model.CampsiteId });
        }

        public IActionResult EditReview(EditReviewModel model)
        {
            if (!Service.CheckReviewOwner(model.Id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            Service.EditReview(model);
;           
            return RedirectToAction("CampsiteDetails", "Map", new { id = model.CampsiteId });
        }
    }
}
