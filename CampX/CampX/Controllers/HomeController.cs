using CampX.BusinessLogic.Implementations.Trips;
using CampX.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CampX.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly TripService Service;
        public HomeController(ILogger<HomeController> logger, TripService tripService)
        {
            _logger = logger;
            Service = tripService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Test2()
        {
            var models = Service.ShowTrips();

            return View("Test2", models);
        }

        [HttpGet]
        public IActionResult Test1()
        {
            var models = Service.GetFinishedTrips();

            return View("Test2", models);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Error_Unauthorized()
        {
            return View();// "Shared/Error_Unauthorized");
        }
    }
}   