using CampX.BusinessLogic.Implementations.Requests;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Reviews;
using CampX.Code.Base;
using CampX.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CampX.Controllers
{

    public class RequestController : BaseController
    {
        private readonly RequestService Service;
        public RequestController(ControllerDependencies dependencies, RequestService service)
           : base(dependencies)
        {
            this.Service = service;
        }

        public bool CheckRequest(AddRequestModel model)
        {
            if (Service.CheckRequestDuplicate(model))
            {
                return false;
            }
            return true;
        }
        public IActionResult AddRequest(AddRequestModel model)
        {
            

            Service.AddRequest(model);
            return View();
        }

        public IActionResult ShowRequests()
        {
            if (!currentCamper.IsAuthenticated)
            {
                return RedirectToAction("Login", "CamperAccount");
            }
            var models = Service.ShowRequests();

            return View(models);
        }
    }
}
