using ContactsManager.Filters.ActionFilters;
using ContactsManager.Filters.AuthorizationFilters;
using ContactsManager.Filters.ResultFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace ContactsManager.Controllers
{
    [Route("contacts")]
    [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = ["X-Controller-Key", "MyValue"])]
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
        [TypeFilter(typeof(ContactsListActionFilter))] // [TypeFilter<ContactsListActionFilter>]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = ["X-Index-Key", "MyValue"])]
        [TypeFilter(typeof(ContactsListResultFilter))]
        public async Task<IActionResult> Index(string searchBy, string? search,
            string sortBy = nameof(PersonResponse.Name),
            SortOrder sortOrder = SortOrder.Ascending)
        {
            // setting the viewdata in the filter action
            //ViewBag.SearchBy = searchBy;
            //ViewBag.Search = search;
            //ViewBag.SearchFields = new Dictionary<string, string>()
            //{
            //    { nameof(PersonResponse.Name), "Name" },
            //    { nameof(PersonResponse.Email), "Email" },
            //    { nameof(PersonResponse.BirthDate), "Birth Date" },
            //    { nameof(PersonResponse.Gender), "Gender" },
            //    { nameof(PersonResponse.CountryId), "Country" },
            //    { nameof(PersonResponse.Address), "Address" }
            //};
            //ViewBag.SortBy = sortBy;
            //ViewBag.SortOrder = sortOrder.ToString();

            var filteredContacts = await _contactsService.GetFilteredContacts(searchBy, search);
            var sortedContacts = await _contactsService.GetSortedContacts(filteredContacts, sortBy, sortOrder);
            return View(sortedContacts);
        }

        [Route("[action]")]
        [HttpGet]
        [TypeFilter(typeof(ContactsHttpPostActionFilter))]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = ["X-Create-Key", "MyValue"])]
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
        [TypeFilter(typeof(ContactsHttpPostActionFilter))]
        public async Task<IActionResult> Create(PersonAddRequest personRequest)
        {
            // logic moved to (short circuiting) action filter
            //if (!ModelState.IsValid)
            //{
            //    // this happens when client side validations fails (very rarely)
            //    var countries = await _countriesService.GetCountries();
            //    ViewBag.Countries = countries.Select(country => new SelectListItem()
            //    {
            //        Text = country.Name,
            //        Value = country.Id.ToString()
            //    });

            //    // using cllient server validation instead
            //    //ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e =>
            //    e.ErrorMessage).ToList();

            //    return View(personRequest);
            //}
            await _contactsService.AddContact(personRequest);

            return RedirectToAction("Index", "Contacts");
        }

        [Route("[action]/{id}")]
        [HttpGet]
        [TypeFilter(typeof(TokenResultFilter))]
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
        [TypeFilter(typeof(TokenAuthorizationFilter))]
        public async Task<IActionResult> Edit(PersonUpdateRequest personRequest)
        {
            var person = await _contactsService.GetContact(personRequest.Id);

            if (person == null)
            {
                return RedirectToAction("Index");
            }

            // modelstate logic moved to (short circuiting) action filter
            //if (ModelState.IsValid)
            //{
            //    // this happens when client side validations fails (very rarely)
            //    var countries = await _countriesService.GetCountries();
            //    ViewBag.Countries = countries.Select(country => new SelectListItem()
            //    {
            //        Text = country.Name,
            //        Value = country.Id.ToString()
            //    });
            //    ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e =>
            //    e.ErrorMessage).ToList();
            //    return View();
            //}
            await _contactsService.UpdateContact(personRequest);
            return RedirectToAction("Index");
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
