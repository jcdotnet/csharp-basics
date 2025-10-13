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
        private readonly List<Person> _contacts;
        private readonly ICountriesService _countriesService;

        public ContactsService(bool initializeWithMockData = true)
        {
            _contacts = new List<Person>();
            _countriesService = new CountriesService();

            if (initializeWithMockData)
            {
                // https://www.mockaroo.com/
                _contacts.AddRange([
                    new Person() {
                        Id = Guid.Parse("7F711F5D-DE40-478D-8DC1-0357D69FE70C"), Name = "Franklyn",
                        Address = "0205 Manley Court", Email="fjewess0@mozilla.org", ReceiveNewsletters= false,
                        BirthDate = Convert.ToDateTime("26/10/2007"), Gender = Gender.Male.ToString(),
                        CountryId = Guid.Parse("6B820C12-FF44-4412-9E14-7941A869FB03")
                    },
                    new Person() {
                        Id = Guid.Parse("5EEBDA1A-3BBA-47E8-A030-5878484A1D01"), Name = "Blinnie",
                        Address = "87 Clemons Road", Email="bphilipeau1@cnet.com", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("29/06/1989"), Gender = Gender.Male.ToString(),
                        CountryId = Guid.Parse("6B820C12-FF44-4412-9E14-7941A869FB03")
                    },
                    new Person() {
                        Id = Guid.Parse("B2BB228D-E31F-47B4-8FE1-2349A09C005A"), Name = "Stanfield",
                        Address = "11213 Pepper Wood Parkway", Email="sdranfield2@weather.com", ReceiveNewsletters= false,
                        BirthDate = Convert.ToDateTime("10/10/1987"), Gender = Gender.Male.ToString(),
                        CountryId = Guid.Parse("D736FB55-4204-41DA-B3DA-144B4C70DD51")
                    },
                    new Person() {
                        Id = Guid.Parse("EA6273DC-0C9A-4E52-B65E-9244DFC7C60B"), Name = "Krisha",
                        Address = "35223 Roxbury Center", Email="klilleycrop3@delicious.com", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("07/07/1997"), Gender = Gender.Male.ToString(),
                        CountryId = Guid.Parse("D736FB55-4204-41DA-B3DA-144B4C70DD51")
                    },
                    new Person() {
                        Id = Guid.Parse("FA1DD467-F430-4BE3-9DC4-ED23A6B4B16E"), Name = "Sebastien",
                        Address = "22 Bowman Pass", Email="sseamer4@prnewswire.com", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("08/10/2007"), Gender = Gender.Male.ToString(),
                        CountryId = Guid.Parse("59C3A91A-E333-409D-8D95-C6E42CCD1680")
                    },
                    new Person() {
                        Id = Guid.Parse("14c0fb52-71fd-4a56-a4bd-42ba072611ac"), Name = "Rubina",
                        Address = "5352 Meadow Ridge Junction", Email="rscrymgeour5@tripadvisor.com", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("20/03/1975"), Gender = Gender.Female.ToString(),
                        CountryId = Guid.Parse("59C3A91A-E333-409D-8D95-C6E42CCD1680")
                    },
                    new Person() {
                        Id = Guid.Parse("ff6db138-c462-48ad-86c4-789aecae8903"), Name = "Gerianna",
                        Address = "13480 Browning Way", Email="gtempleton6@about.me", ReceiveNewsletters= false,
                        BirthDate = Convert.ToDateTime("13/11/1999"), Gender = Gender.Female.ToString(),
                        CountryId = Guid.Parse("28DC009E-FC31-4598-A5A5-692F3E84E0CD")
                    },
                    new Person() {
                        Id = Guid.Parse("a78c2bbe-4037-4d9b-8db0-6ea251ece14d"), Name = "Veronike",
                        Address = "99 Arrowood Crossing", Email="vshowering7@themeforest.net", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("10/06/2000"), Gender = Gender.Female.ToString(),
                        CountryId = Guid.Parse("28DC009E-FC31-4598-A5A5-692F3E84E0CD")
                    },
                    new Person() {
                        Id = Guid.Parse("19110652-d1be-45a1-8a2e-e9703d09496d"), Name = "Merill",
                        Address = "06224 Straubel Place", Email="mstebbings8@godaddy.com", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("03/02/2007"), Gender = Gender.Male.ToString(),
                        CountryId = Guid.Parse("FFD79E28-72C6-4181-A5EB-F09B02252CD6")
                    },
                    new Person() {
                        Id = Guid.Parse("65ba3b4f-0f20-4251-ad75-b38cc01d0769"), Name = "Jasen",
                        Address = "726 Eastwood Center", Email="jfunnell9@e-recht24.de", ReceiveNewsletters= true,
                        BirthDate = Convert.ToDateTime("04/08/2009"), Gender = Gender.Other.ToString(),
                        CountryId = Guid.Parse("FFD79E28-72C6-4181-A5EB-F09B02252CD6")
                    }
                ]);
            }
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

        public bool DeleteContact(Guid? id)
        {
            if (id == null) throw new ArgumentNullException(nameof(id));

            Person? person = _contacts.FirstOrDefault(p => p.Id == id);
            if (person == null) { return false; }

            _contacts.RemoveAll(p => p.Id == id);

            return true;
        }

        public PersonResponse? GetContact(Guid? id)
        {
            if (id == null) return null;

            Person? person = _contacts.FirstOrDefault(p => p.Id == id);

            if (person == null) { return null; }

            return ConvertToPersonResponse(person);
        }

        public List<PersonResponse> GetContacts()
        {
            return _contacts.Select(c => ConvertToPersonResponse(c)).ToList();
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

            Person? person = _contacts.FirstOrDefault(p => p.Id == personDto.Id);
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
