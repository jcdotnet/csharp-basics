using Microsoft.AspNetCore.Mvc;
using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.ServiceContracts;

namespace ResortBookingApp.Web.Controllers
{
    public class VillasController : Controller
    {
        private readonly IVillaService _service;

        public VillasController(IVillaService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var villas = await _service.GetVillas();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(VillaAddRequest villa)
        {
            if (villa.Name == villa.Description) // custom model validation
            {
                ModelState.AddModelError("Name", "Name cannot be equals to description");
            }
            if (ModelState.IsValid)
            {
                _service.AddVilla(villa);
                TempData["success"] = "The villa has been created successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Update(int? id)
        {
            VillaResponse? villa = await _service.GetVilla(id);

            if (villa == null)
            {
                //return NotFound();
                return RedirectToAction("Error", "Home");
            }

            return View(villa.ToVillaUpdateRequest());
        }

        [HttpPost]
        public IActionResult Update(VillaUpdateRequest villa)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateVilla(villa);
                TempData["success"] = "The villa has been updated successfully.";
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var villa = await _service.GetVilla(id);
            if (villa == null)
            {
                //return NotFound();
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(VillaResponse villa)
        {
            bool deleted = await _service.DeleteVilla(villa.Id);
            if (!deleted)
            {
                TempData["error"] = "Failed to delete the villa.";
                return View(villa);
            }
            TempData["success"] = "The villa has been deleted successfully.";
            return RedirectToAction("Index");

        }
    }
}
