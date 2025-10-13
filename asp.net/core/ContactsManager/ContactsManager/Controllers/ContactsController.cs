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
        public IActionResult Index(string searchBy, string? search,
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

            var filteredContacts = _contactsService.GetFilteredContacts(searchBy, search);
            var sortedContacts = _contactsService.GetSortedContacts(filteredContacts, sortBy, sortOrder);
            return View(sortedContacts);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            var countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(country => new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id.ToString()
            });
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(PersonAddRequest personAddRequest)
        {
            if (!ModelState.IsValid)
            {
                // this happens when client side validations fails (very rarely)
                var countries = _countriesService.GetAllCountries();
                ViewBag.Countries = countries.Select(country => new SelectListItem()
                {
                    Text = country.Name,
                    Value = country.Id.ToString()
                });

                // using cllient server validation instead
                //ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

                return View();
            }
            _contactsService.AddContact(personAddRequest);

            return RedirectToAction("Index", "Contacts");
        }

        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var person = _contactsService.GetContact(id); // PersonResponse?
            if (person == null)
            {
                return RedirectToAction("Index");
            }
            var countries = _countriesService.GetAllCountries();
            ViewBag.Countries = countries.Select(country => new SelectListItem()
            {
                Text = country.Name,
                Value = country.Id.ToString()
            });
            return View(person.ToPersonUpdateRequest());
        }
        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            var person = _contactsService.GetContact(personUpdateRequest.Id); // PersonResponse?

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                _contactsService.UpdateContact(personUpdateRequest);
                return RedirectToAction("Index");
            }
            else
            {
                // this happens when client side validations fails (very rarely)
                var countries = _countriesService.GetAllCountries();
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
        public IActionResult Delete(Guid? id)
        {
            var person = _contactsService.GetContact(id); // PersonResponse?
            if (person == null)
                return RedirectToAction("Index");

            return View(person);
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public IActionResult Delete(PersonUpdateRequest personUpdateRequest)
        {
            var person = _contactsService.GetContact(personUpdateRequest.Id);
            if (person == null)
                return RedirectToAction("Index");

            _contactsService.DeleteContact(personUpdateRequest.Id);
            return RedirectToAction("Index");
        }
    }
}
