using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services.Helpers;

namespace Services
{
    public class ContactsService : IContactsService
    {
        private readonly List<Person> _contacts;
        private readonly ICountriesService _countriesService;

        public ContactsService()
        {
            _contacts = new List<Person>();
            _countriesService = new CountriesService();
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

            _contacts.Add(person);

            return ConvertToPersonResponse(person);
        }

        public PersonResponse? GetContact(Guid? id)
        {
            if (id == null) return null;

            Person? person = _contacts.FirstOrDefault(p => p.Id == id);

            return person?.ToPersonResponse();
        }

        public List<PersonResponse> GetContacts()
        {
            return _contacts.Select(c => c.ToPersonResponse()).ToList();
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

        private PersonResponse ConvertToPersonResponse(Person person)
        {
            PersonResponse personResponseDto = person.ToPersonResponse();
            personResponseDto.Country = _countriesService.GetCountry(person.CountryId)?.Name;
            return personResponseDto;
        }
    }
}
