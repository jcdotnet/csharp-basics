using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ContactsManager.Controllers
{
    [Route("contacts")]
    public class ContactsController : Controller
    {

        private ICountriesService _countriesService;
        private IContactsService _contactsService;

        public ContactsController(ICountriesService countriesService, IContactsService contactsService)
        {
            _contactsService = contactsService;
            _countriesService = countriesService;
        }

        [Route("/")]
        // [Route("index")]
        [Route("[action]")] // route token
        public async Task<IActionResult> Index(string searchBy, string? search,
            string sortBy = nameof(PersonResponse.Name),
            SortOrder sortOrder = SortOrder.Ascending)
        {
            ViewBag.SearchBy = searchBy;
            ViewBag.Search = search;
            ViewBag.SearchFields = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.Name), "Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.BirthDate), "Birth Date" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.CountryId), "Country" },
                { nameof(PersonResponse.Address), "Address" }
            };

            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder.ToString();

            var filteredContacts = await _contactsService.GetFilteredContacts(searchBy, search);
            var sortedContacts = await _contactsService.GetSortedContacts(filteredContacts, sortBy, sortOrder);
            return View(sortedContacts);
        }

        [Route("[action]")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var countries = await _countriesService.GetCountries();
            ViewBag.Countries = countries.Select(country => new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id.ToString()
            });
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                // this happens when client side validations fails (very rarely)
                var countries = await _countriesService.GetCountries();
                ViewBag.Countries = countries.Select(country => new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id.ToString()
                });

                // using cllient server validation instead
                //ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View(personAddRequest);
            }
            await _contactsService.AddContact(personAddRequest);

            return RedirectToAction("Index", "Contacts");
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var person = await _contactsService.GetContact(id); 
            if (person == null)
            {
                return RedirectToAction("Index");
            }
            var countries = await _countriesService.GetCountries();
            ViewBag.Countries = countries.Select(country => new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id.ToString()
            });
            return View(person.ToPersonUpdateRequest());
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Edit(PersonUpdateRequest personUpdateRequest)
        {
            var person = await _contactsService.GetContact(personUpdateRequest.Id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                await _contactsService.UpdateContact(personUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                // this happens when client side validations fails (very rarely)
                var countries = await _countriesService.GetCountries();
                ViewBag.Countries = countries.Select(country => new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id.ToString()
                }); 
                //ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            var person = await _contactsService.GetContact(id);
            if (person == null)
                return RedirectToAction("Index");

            return View(person);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Delete(PersonUpdateRequest personUpdateRequest)
        {
            var person = await _contactsService.GetContact(personUpdateRequest.Id);
            if (person == null)
                return RedirectToAction("Index");

            await _contactsService.DeleteContact(personUpdateRequest.Id);
            return RedirectToAction("Index");
        }
    }
}
