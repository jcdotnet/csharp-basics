using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.ServiceContracts;
using ResortBookingApp.Application.Utility;

namespace ResortBookingApp.Web.Controllers
{
    [Authorize(Roles = SD.RoleAdmin)]
    public class AmenitiesController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IAmenityService _service;

        public AmenitiesController(IAmenityService service, IVillaService villaService)
        {
            _service = service;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            var amenities = await _service.GetAmenities();
            return View(amenities);
        }

        public async Task<IActionResult> Create()
        {
            IEnumerable<SelectListItem> list = (await _villaService.GetVillas()).Select(v =>
                new SelectListItem()
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }
            );
            ViewBag.VillasList = list;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AmenityAddRequest amenity)
        {
            if (ModelState.IsValid )
            {
                await _service.AddAmenity(amenity);
                TempData["success"] = "The amenity has been created successfully.";
                return RedirectToAction("Index");
            }
            // model invalid
            ViewBag.VillasList = (await _villaService.GetVillas()).Select(v =>
                new SelectListItem()
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }
            );
            return View(amenity);
        }

        public async Task<IActionResult> Update(int? id)
        {

            AmenityResponse? amenity = await _service.GetAmenity(id);

            if (amenity is null)
            {
                //return NotFound();
                return RedirectToAction("Error", "Home");
            }

            ViewBag.VillasList = (await _villaService.GetVillas()).Select(v =>
              new SelectListItem()
              {
                  Text = v.Name,
                  Value = v.Id.ToString()
              }
            );
            return View(amenity.ToAmenityUpdateRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Update(AmenityUpdateRequest amenity)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAmenity(amenity);
                TempData["success"] = "The amenity has been updated successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.VillasList = (await _villaService.GetVillas()).Select(v =>
                new SelectListItem()
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }
            );
            return View(amenity);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var amenity = await _service.GetAmenity(id);
            if (amenity == null)
            {
                //return NotFound();
                return RedirectToAction("Error", "Home");
            }
            return View(amenity);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(AmenityResponse amenity)
        {
            bool deleted = await _service.DeleteAmenity(amenity.Id);
            if (!deleted)
            {
                TempData["error"] = "Failed to delete the amenity.";
                return View(amenity);
            }
            TempData["success"] = "The amenity has been deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
