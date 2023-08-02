using CampX.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CampX.Code.Base
{
    public class BaseController : Controller
    {
        protected readonly CurrentCamperDTO currentCamper;

        public BaseController(ControllerDependencies dependencies)
            : base ()
        {
            currentCamper  = dependencies.CurrentCamper;
        }
    }
}
