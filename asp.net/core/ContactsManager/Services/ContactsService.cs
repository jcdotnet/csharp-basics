using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System;

namespace Services
{
    public class ContactsService : IContactsService
    {
        private readonly ApplicationDbContext _db;

        public ContactsService(ApplicationDbContext contactsManagerDbContext)
        {
            _db = contactsManagerDbContext;
        }

        public async Task<PersonResponse> AddContact(PersonAddRequest? personDto)
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
            await _db.SaveChangesAsync(); // DDL

            // stored procedure
            //_db.sp_InsertPerson(person);

            return person.ToPersonResponse();
        }

        public async Task<bool> DeleteContact(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Person? person = _db.People.FirstOrDefault(p => p.Id == id);
            if (person == null) { return false; }

            _db.People.Remove(_db.People.First(p => p.Id == id));
            await _db.SaveChangesAsync();

            return true;
        }

        public async Task<PersonResponse?> GetContact(Guid? id)
        {
            if (id == null) return null;

            Person? person = await _db.People.Include("Country").FirstOrDefaultAsync(p => p.Id == id);

            if (person == null) { return null; }

            return person.ToPersonResponse();
        }

        public async Task<List<PersonResponse>> GetContacts()
        {
            // InvalidOperationException
            //return _db.People.Select(c => c.ToPersonResponse()).ToList();

            // first db operations (Select * from People), then user methods
            return (await _db.People.Include("Country").ToListAsync()).Select(c => c.ToPersonResponse()).ToList();

            // stored procedure
            // Include: the data returned from the stored procedure is mapped
            // to entities and not directly loaded into navigation properties
            //return _db.sp_GetPeople().Select(c => c.ToPersonResponse()).ToList();
        }

        public async Task<List<PersonResponse>> GetFilteredContacts(string searchBy, string? search)
        {
            List<PersonResponse> getAll = await GetContacts();
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

        public async Task<List<PersonResponse>> GetSortedContacts(List<PersonResponse> contacts, string sortBy, SortOrder sortOrder)
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

        public async Task<PersonResponse> UpdateContact(PersonUpdateRequest? personDto)
        {
            if (personDto == null)
                throw new ArgumentNullException(nameof(Person));

            ValidationHelper.ModelValidation(personDto);

            Person? person = await _db.People.FirstOrDefaultAsync(p => p.Id == personDto.Id);
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

            await _db.SaveChangesAsync();

            return person.ToPersonResponse();
        }

    }
}
