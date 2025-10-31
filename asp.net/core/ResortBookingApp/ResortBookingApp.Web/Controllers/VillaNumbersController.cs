using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResortBookingApp.Application.DTO;
using ResortBookingApp.Application.ServiceContracts;

namespace ResortBookingApp.Web.Controllers
{
    public class VillaNumbersController : Controller
    {
        private readonly IVillaService _villaService;
        private readonly IVillaNumberService _service;

        public VillaNumbersController(IVillaNumberService service, IVillaService villaService)
        {
            _service = service;
            _villaService = villaService;
        }

        public async Task<IActionResult> Index()
        {
            var villaNumbers = await _service.GetVillaNumbers();
            return View(villaNumbers);
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

            ViewBag.VillasList = list; // we can use a view model for this instead  
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VillaNumberAddRequest villaNumber)
        {
            bool numberExists = await _service.VillaNumberExists(villaNumber?.Number);
            if (numberExists)
            {
                TempData["error"] = "The villa number already exists.";
            }
            if (ModelState.IsValid && !numberExists)
            {
                await _service.AddVillaNumber(villaNumber);
                TempData["success"] = "The villa number has been created successfully.";
                return RedirectToAction("Index");
            }
            // model invalid or number exists
            ViewBag.VillasList = (await _villaService.GetVillas()).Select(v =>
                new SelectListItem()
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }
            );
            return View(villaNumber);
        }

        public async Task<IActionResult> Update(int? id)
        {

            VillaNumberResponse? villa = await _service.GetVillaNumber(id);

            if (villa is null)
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
            return View(villa.ToVillaNumberUpdateRequest());
        }

        [HttpPost]
        public async Task<IActionResult> Update(VillaNumberUpdateRequest updateRequest)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateVillaNumber(updateRequest);
                TempData["success"] = "The villa number has been updated successfully.";
                return RedirectToAction("Index");
            }
            ViewBag.VillasList = (await _villaService.GetVillas()).Select(v =>
                new SelectListItem()
                {
                    Text = v.Name,
                    Value = v.Id.ToString()
                }
            );
            return View(updateRequest);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var villa = await _service.GetVillaNumber(id);
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
            bool deleted = await _service.DeleteVillaNumber(villa.Id);
            if (!deleted)
            {
                TempData["error"] = "Failed to delete the villa.";
                return View(villa);
            }
            TempData["success"] = "The villa number has been deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
