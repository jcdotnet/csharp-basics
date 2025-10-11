using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index(string searchBy, string? search, 
            string sortBy = nameof(PersonResponse.Name), 
            SortOrder sortOrder = SortOrder.Ascending)
        {
            ViewBag.SearchBy        = searchBy;
            ViewBag.Search          = search;
            ViewBag.SearchFields    = new Dictionary<string, string>()
            {
                { nameof(PersonResponse.Name), "Name" },
                { nameof(PersonResponse.Email), "Email" },
                { nameof(PersonResponse.BirthDate), "Birth Date" },
                { nameof(PersonResponse.Gender), "Gender" },
                { nameof(PersonResponse.CountryId), "Country" },
                { nameof(PersonResponse.Address), "Address" }
            };

            ViewBag.SortBy          = sortBy;
            ViewBag.SortOrder       = sortOrder.ToString();

            var filteredContacts    = _contactsService.GetFilteredContacts(searchBy, search);
            var sortedContacts      = _contactsService.GetSortedContacts(filteredContacts, sortBy, sortOrder);
            return View(sortedContacts);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.countries = _countriesService.GetAllCountries();
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.countries = _countriesService.GetAllCountries();
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View();
            }
            _contactsService.AddContact(personAddRequest);

            return RedirectToAction("Index", "Contacts");
        }
    }
}
