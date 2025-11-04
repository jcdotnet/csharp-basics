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

        [HttpPost]
        public async Task<IActionResult> GetVillasByDate(int nights, DateOnly checkInDate)
        {
            var villasList = await _villaService.GetVillas();
            foreach (var villa in villasList)
            {
                if (villa.Id % 2 == 0)
                {
                    villa.IsAvailable = false; // to change later
                }
            }
            HomeViewModel model = new()
            {
                VillasList = villasList,
                CheckInDate = checkInDate,
                Nights = nights
            };
            return PartialView("_VillasListPartial", model); //View(model);
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
