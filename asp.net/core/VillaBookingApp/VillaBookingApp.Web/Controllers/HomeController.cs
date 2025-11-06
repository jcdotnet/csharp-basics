using Microsoft.AspNetCore.Mvc;
using VillaBookingApp.Application.ServiceContracts;
using VillaBookingApp.Application.Utility;
using VillaBookingApp.Web.Models;

namespace VillaBookingApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _villaNumberService;
        private readonly IBookingService _bookingService;

        public HomeController(ILogger<HomeController> logger, IVillaService villaService,
            IVillaNumberService villaNumberService, IBookingService bookingService)
        {
            _logger = logger;
            _villaService = villaService;
            _villaNumberService = villaNumberService;
            _bookingService = bookingService;
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
            var villaNumbersList = await _villaNumberService.GetVillaNumbers();
            var bookedVillas = (await _bookingService.GetBookedVillas()).ToList();
            foreach (var villa in villasList)
            {
                int roomsAvailabe = SD.RoomsAvailable(
                    villa.Id, villaNumbersList, checkInDate, nights, bookedVillas
                );
                villa.IsAvailable = roomsAvailabe > 0;
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
