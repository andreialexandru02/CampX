using CampX.BusinessLogic.Implementations.Map;
using CampX.BusinessLogic.Implementations.Reviews;
using CampX.BusinessLogic.Implementations.Reviews.Models;
using CampX.Code.Base;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace CampX.Controllers
{
    public class ReviewController : BaseController
    {

        private readonly ReviewService Service;

        public ReviewController(ControllerDependencies dependencies, ReviewService service)
           : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]

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

        public IActionResult DeleteReview(DeleteReviewModel  model)
        {

            Service.DeleteReview(model.Id);

            return RedirectToAction("CampsiteDetails", "Map", new { id = model.CampsiteId});

        }

        [HttpPost]

        public IActionResult AddReview(AddReviewModel model)
        {
            Service.AddReview(model);

            return RedirectToAction("CampsiteDetails", "Map", new { id = model.CampsiteId });
        }
    }
}
