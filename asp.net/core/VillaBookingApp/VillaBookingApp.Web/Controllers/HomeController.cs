using Microsoft.AspNetCore.Mvc;
using VillaBookingApp.Application.ServiceContracts;
using VillaBookingApp.Web.Models;

namespace VillaBookingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;

        public HomeController(ILogger<HomeController> logger, IVillaService villaService)
        {
            _logger = logger;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            HomeViewModel model = new()
            {
                VillasList = await _villaService.GetVillas(),
                CheckInDate = DateOnly.FromDateTime(DateTime.Now),
                Nights = 1
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
