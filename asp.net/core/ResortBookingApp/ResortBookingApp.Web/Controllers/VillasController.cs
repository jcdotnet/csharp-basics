using Microsoft.AspNetCore.Mvc;
using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.ServiceContracts;

namespace ResortBookingApp.Web.Controllers
{
    public class VillasController : Controller
    {
        private readonly IVillaService _service;
        private readonly IWebHostEnvironment _environment;

        public VillasController(IVillaService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
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
                // save image
                if (villa.Image != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(villa.Image.FileName);
                    string imageDir = Path.Combine(_environment.WebRootPath, @"images\villas");
                    string imagePath = Path.Combine(imageDir, fileName);

                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    villa.Image.CopyTo(fileStream);
                    villa.ImageUrl = @"\images\villas\" + fileName;
                } else
                {
                    villa.ImageUrl = "https://placehold.co/600x400";
                }
                // add villa to the databasa
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
                // save image and remove old one (if any)
                if (villa.Image != null)
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(villa.Image.FileName);
                    string imageDir = Path.Combine(_environment.WebRootPath, @"images\villas");
                    string imagePath = Path.Combine(imageDir, fileName);

                    if (!string.IsNullOrEmpty(villa.ImageUrl))
                    {
                        string olgImagePath = Path.Combine(
                            _environment.WebRootPath, 
                            villa.ImageUrl.TrimStart('\\')
                        );
                        if (System.IO.File.Exists(olgImagePath))
                        {
                            System.IO.File.Delete(olgImagePath);
                        }
                    }
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    villa.Image.CopyTo(fileStream);
                    villa.ImageUrl = @"\images\villas\" + fileName;
                }
                else
                {
                    villa.ImageUrl = "https://placehold.co/600x400";
                }
                // update villa
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
            // delete image file from the server
            if (!string.IsNullOrEmpty(villa.ImageUrl))
            {
                string olgImagePath = Path.Combine(
                    _environment.WebRootPath,
                    villa.ImageUrl.TrimStart('\\')
                );
                if (System.IO.File.Exists(olgImagePath))
                {
                    System.IO.File.Delete(olgImagePath);
                }
            }
            TempData["success"] = "The villa has been deleted successfully.";
            return RedirectToAction("Index");

        }
    }
}
