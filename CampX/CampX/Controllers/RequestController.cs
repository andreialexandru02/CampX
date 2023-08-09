using CampX.BusinessLogic.Implementations.Requests;
using CampX.BusinessLogic.Implementations.Requests.Models;
using CampX.BusinessLogic.Implementations.Reviews;
using CampX.Code.Base;
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

        public IActionResult AddRequest(AddRequestModel model)
        {
            
            Service.AddRequest(model);
            return View();
        }
    }
}
