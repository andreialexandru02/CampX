using CampX.BusinessLogic.Implementations.Images;
using CampX.Code.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampX.Controllers
{
    [Authorize]
    public class ImageController : BaseController
    {
        private readonly ImagesService service;
        public ImageController(ControllerDependencies dependencies, ImagesService service) : base(dependencies)
        {
            this.service = service;
        }

        public IActionResult GetImgContent(int id)
        {
            var model = service.GetImgContent(id);

            return File(model, "image/jpg");
        }
    }

}
