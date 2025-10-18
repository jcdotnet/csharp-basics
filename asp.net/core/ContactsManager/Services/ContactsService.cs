using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;
using System;
using System.Threading.Channels;

namespace Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _repository;

        public ContactsService(IContactsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PersonResponse> AddContact(PersonAddRequest? personDto)
        {
            if (personDto == null)
            {
                throw new ArgumentNullException(nameof(personDto));
            }

            ValidationHelper.ModelValidation(personDto);

            Person person = personDto.ToPerson();
            person.Id = Guid.NewGuid();

            await _repository.AddContact(person);

            return person.ToPersonResponse();
        }

        public async Task<bool> DeleteContact(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Person? person = await _repository.GetContact(id.Value);
            if (person == null) { return false; }

            await _repository.DeleteContact(id.Value);

            return true;
        }
        
        public async Task<PersonResponse?> GetContact(Guid? id)
        {
            if (id == null) return null;

            Person? person = await _repository.GetContact(id.Value);

            if (person == null) { return null; }

            return person.ToPersonResponse();
        }
        
        public async Task<List<PersonResponse>> GetContacts()
        {
            return (await _repository.GetContacts()).Select(c => c.ToPersonResponse()).ToList();    
        }

        public async Task<List<PersonResponse>> GetFilteredContacts(string searchBy, string? search)
        {
            List<Person> getFiltered;
            if (string.IsNullOrEmpty(search)) getFiltered = await _repository.GetContacts();
            else 
                getFiltered = searchBy switch
                {
                    nameof(PersonResponse.Name) => await _repository.GetFilteredContacts(p =>
                        p.Name.Contains(search)),
                    nameof(PersonResponse.Email) => await _repository.GetFilteredContacts(p =>
                        p.Email.Contains(search)),
                    nameof(PersonResponse.Address) => await _repository.GetFilteredContacts(p =>
                        p.Address.Contains(search)),
                    nameof(PersonResponse.BirthDate) => await _repository.GetFilteredContacts(p =>
                        p.BirthDate.Value.ToString("dd MMMM yyyy").Contains(search)),
                    nameof(PersonResponse.Gender) => await _repository.GetFilteredContacts(p =>
                        p.Gender.Contains(search)),
                    nameof(PersonResponse.CountryId) => await _repository.GetFilteredContacts(p =>
                        p.Country.Name.Contains(search)),
                    _ => await _repository.GetContacts()
                };
            return getFiltered.Select(p => p.ToPersonResponse()).ToList();
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

            Person? person = await _repository.GetContact(personDto.Id);
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

            await _repository.UpdateContact(person);

            return person.ToPersonResponse();
        }
    }
}
