using Microsoft.AspNetCore.Mvc;

namespace VillaBookingApp.Web.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
