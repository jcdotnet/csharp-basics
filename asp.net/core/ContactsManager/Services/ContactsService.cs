using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Services
{
    public class ContactsService : IContactsService
    {
        private readonly ContactsManagerDbContext _db;
        private readonly ICountriesService _countriesService;

        public ContactsService(ContactsManagerDbContext contactsManagerDbContext,
            ICountriesService countriesService)
        {
            _db = contactsManagerDbContext;
            _countriesService = countriesService;
        }

        public PersonResponse AddContact(PersonAddRequest? personDto)
        {
            if (personDto == null)
            {
                throw new ArgumentNullException(nameof(personDto));
            }

            /*
            if (string.IsNullOrEmpty(personDto.Name))
            {
                throw new ArgumentException(nameof(personDto.Name));
            }*/
            ValidationHelper.ModelValidation(personDto);

            Person person = personDto.ToPerson();
            person.Id = Guid.NewGuid();

            _db.People.Add(person);
            _db.SaveChanges(); // DDL

            return ConvertToPersonResponse(person);
        }

        public bool DeleteContact(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Person? person = _db.People.FirstOrDefault(p => p.Id == id);
            if (person == null) { return false; }

            _db.People.Remove(_db.People.First(p => p.Id == id));
            _db.SaveChanges();

            return true;
        }

        public PersonResponse? GetContact(Guid? id)
        {
            if (id == null) return null;

            Person? person = _db.People.FirstOrDefault(p => p.Id == id);

            if (person == null) { return null; }

            return ConvertToPersonResponse(person);
        }

        public List<PersonResponse> GetContacts()
        {
            // InvalidOperationException
            //return _db.People.Select(c => ConvertToPersonResponse(c)).ToList();
            // first db operations (Select * from People), then user methods
            return _db.People.ToList().Select(c => ConvertToPersonResponse(c)).ToList();
        }

        public List<PersonResponse> GetFilteredContacts(string searchBy, string? search)
        {
            List<PersonResponse> getAll = GetContacts();
            List<PersonResponse> getFiltered = getAll;

            if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(search))
                return getFiltered;

            switch (searchBy)
            {
                case nameof(Person.Name):
                    getFiltered = getAll.Where(p =>
                    (!string.IsNullOrEmpty(p.Name) ?
                    p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Email):
                    getFiltered = getAll.Where(p =>
                    (!string.IsNullOrEmpty(p.Email) ?
                    p.Email.Contains(search, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.Address):
                    getFiltered = getAll.Where(p =>
                    (!string.IsNullOrEmpty(p.Address) ?
                    p.Address.Contains(search, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.BirthDate):
                    getFiltered = getAll.Where(p =>
                    (p.BirthDate != null) ?
                    p.BirthDate.Value.ToString("dd MMMM yyyy").Contains(search, StringComparison.OrdinalIgnoreCase) : true).ToList();
                    break;

                case nameof(Person.Gender):
                    getFiltered = getAll.Where(p =>
                    (!string.IsNullOrEmpty(p.Gender) ?
                    p.Gender.Contains(search, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                case nameof(Person.CountryId):
                    getFiltered = getAll.Where(p =>
                    (!string.IsNullOrEmpty(p.Country) ?
                    p.Country.Contains(search, StringComparison.OrdinalIgnoreCase) : true)).ToList();
                    break;

                default: getFiltered = getAll; break;
            }
            return getFiltered;
        }

        public List<PersonResponse> GetSortedContacts(List<PersonResponse> contacts, string sortBy, SortOrder sortOrder)
        {
            if (string.IsNullOrEmpty(sortBy))
                return contacts;

            List<PersonResponse> getSorted = (sortBy, sortOrder) switch
            {
                (nameof(PersonResponse.Name), SortOrder.Ascending) => contacts.OrderBy(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Name), SortOrder.Descending) => contacts.OrderByDescending(p => p.Name, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrder.Ascending) => contacts.OrderBy(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Email), SortOrder.Descending) => contacts.OrderByDescending(p => p.Email, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrder.Ascending) => contacts.OrderBy(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Address), SortOrder.Descending) => contacts.OrderByDescending(p => p.Address, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.BirthDate), SortOrder.Ascending) => contacts.OrderBy(p => p.BirthDate).ToList(),

                (nameof(PersonResponse.BirthDate), SortOrder.Descending) => contacts.OrderByDescending(p => p.BirthDate).ToList(),

                (nameof(PersonResponse.Age), SortOrder.Ascending) => contacts.OrderBy(p => p.Age).ToList(),

                (nameof(PersonResponse.Age), SortOrder.Descending) => contacts.OrderByDescending(p => p.Age).ToList(),

                (nameof(PersonResponse.Gender), SortOrder.Ascending) => contacts.OrderBy(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Gender), SortOrder.Descending) => contacts.OrderByDescending(p => p.Gender, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrder.Ascending) => contacts.OrderBy(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                (nameof(PersonResponse.Country), SortOrder.Descending) => contacts.OrderByDescending(p => p.Country, StringComparer.OrdinalIgnoreCase).ToList(),

                _ => contacts
            };

            return getSorted;
        }

        public PersonResponse UpdateContact(PersonUpdateRequest? personDto)
        {
            if (personDto == null)
                throw new ArgumentNullException(nameof(Person));

            ValidationHelper.ModelValidation(personDto);

            Person? person = _db.People.FirstOrDefault(p => p.Id == personDto.Id);
            if (person == null)
            {
                throw new ArgumentException("Person does not exist");
            }
            person.Name = personDto.Name;
            person.Email = personDto.Email;
            person.BirthDate = personDto.BirthDate;
            person.Gender = personDto.Gender.ToString();
            person.CountryId = personDto.CountryId;
            person.Address = personDto.Address;
            person.ReceiveNewsletters = personDto.ReceiveNewsletters;

            _db.SaveChanges();

            return ConvertToPersonResponse(person);
        }

        private PersonResponse ConvertToPersonResponse(Person person)
        {
            PersonResponse personResponseDto = person.ToPersonResponse();
            personResponseDto.Country = _countriesService.GetCountry(person.CountryId)?.Name;
            return personResponseDto;
        }
    }
}
