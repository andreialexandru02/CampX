
using CampX.BusinessLogic.Implementations.Account;
using CampX.BusinessLogic.Implementations.Account.Models;
using CampX.Code.Base;
using CampX.Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CampX.Controllers
{
    
    public class CamperAccountController : BaseController
    {
        private readonly CamperAccountService Service;

        public CamperAccountController(ControllerDependencies dependencies, CamperAccountService service)
           : base(dependencies)
        {
            this.Service = service;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var model = new RegisterModel();

            return View("Register", model);
        }
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            return View(model);

        }

        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            Service.RegisterNewCamper(model);

            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }

            Service.ChangePassword(model);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var camper = Service.Login(model.Email, model.Password);
            if (camper.IsBanned)
            {
                return View("YouHaveBeenBanned"); 
            }
            if (!camper.IsAuthenticated)
            {
                model.AreCredentialsInvalid = true;
                return View(model);
            }

            await LogIn(camper);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult YouHaveBeenBanned()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await LogOut();
            
            return RedirectToAction("Index", "Home");
        }

        /*[HttpGet]
        public IActionResult DemoPage()
        {
            var model = Service.GetCampers();

            return View(model);
        }*/
        
        [HttpGet]
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }

        private async Task LogIn(CurrentCamperDTO camper)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", camper.Id.ToString()),
                new Claim("FirstName", camper.FirstName),
                new Claim("LastName", camper.LastName),
                new Claim("Email", camper.Email)
            };

            camper.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var identity = new ClaimsIdentity(claims, "Cookies");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                    scheme: "CampXCookies",
                    principal: principal);
        }
        [Authorize]
        private async Task LogOut()
        {
            await HttpContext.SignOutAsync(scheme: "CampXCookies");
        }
        [Authorize]
        public IActionResult CamperProfile(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            var model = Service.GetCamperProfile(id);
            return View("CamperProfile", model);
        }

        [HttpGet]
        [Authorize]
        public IActionResult EditCamper(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            if (!Service.CheckCamperOwner(id))
            {
                return RedirectToAction("Error_Unauthorized", "Home");
            }
            var model = Service.EditCamper(id);

            return View(model);

            
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditCamper(EditCamperModel model)
        {
            if (model == null)
            {
                return View("Error_NotFound");
            }
            @Service.EditCamper(model);
            return RedirectToAction("CamperProfile", new { id = model.Id});
        }

 
        [Authorize(Policy = "RequireModeratorRole")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult BanCamper(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            Service.BanCamper(id);

            return RedirectToAction("CamperProfile", new { id = id });
        }
 
        [Authorize(Policy = "RequireModeratorRole")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult UnBanCamper(int id)
        {
            if (!Service.IdExists(id))
            {
                return View("Error_NotFound");
            }
            Service.UnBanCamper(id);

            return RedirectToAction("CamperProfile", new { id = id });
        }

    }
}