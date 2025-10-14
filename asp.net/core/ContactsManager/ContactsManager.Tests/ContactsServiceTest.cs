using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Tests
{
    public class ContactsServiceTest
    {
        private readonly IContactsService _service;
        private readonly ICountriesService _countriesService;

        public ContactsServiceTest()
        {
            _countriesService = new CountriesService(
                new ContactsManagerDbContext(new DbContextOptionsBuilder<ContactsManagerDbContext>().Options)
            );
            _service = new ContactsService(
                new ContactsManagerDbContext(new DbContextOptionsBuilder<ContactsManagerDbContext>().Options),
                _countriesService
            );
        }

        #region AddContact
        // Requirement: when person is null, it should throw ArgumentNullException
        [Fact]
        public void AddContact_NullPerson()
        {
            // Arrange
            PersonAddRequest? person = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _service.AddContact(person);
            });
        }

        // Requirement: when person name is null, it should throw ArgumentException
        // Optional: same with email addresses, etc.
        [Fact]
        public void AddContact_NullPersonName()
        {
            // Arrange
            PersonAddRequest? person = new PersonAddRequest() { Name = null };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.AddContact(person);
            });
        }

        // Requirement: when proper person details, it should insert it in the contacts
        [Fact]
        public void AddContact()
        {
            // Arrange
            PersonAddRequest? personRequest = new PersonAddRequest() { Name = "John Doe" };

            // Act
            PersonResponse? personResponse= _service.AddContact(personRequest);
            List<PersonResponse> listResponse = _service.GetContacts();

            // Assert
            Assert.True(personResponse.Id != Guid.Empty);
            Assert.Contains(personResponse, listResponse);
        }
        #endregion

        #region GetContact

        // Requirement: when person id is null, it should return null
        [Fact]
        public void GetContact_NullPersonId()
        {
            // Arrange
            // Act
            PersonResponse? personGetResponse = _service.GetContact(null);

            // Assert
            Assert.Null(personGetResponse);
        }

        [Fact]
        public void GetContact()
        {
            // Arrange
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            CountryResponse country = _countriesService.AddCountry(countryRequest);

            PersonAddRequest? personRequest = new PersonAddRequest() { Name = "John Doe", CountryId = country.Id};
            PersonResponse? personAddResponse = _service.AddContact(personRequest);

            // Act
            PersonResponse? personGetResponse = _service.GetContact(personAddResponse.Id);

            // Assert
            Assert.Equal(personAddResponse, personGetResponse);
        }

        #endregion

        #region GetContacts

        // requirement: the contact list should be emtpy by default
        [Fact]
        public void GetContacts_DefaulEmptytList()
        {
            // Arrange
            // Act
            var contactsList = _service.GetContacts();

            // Assert
            Assert.Empty(contactsList);
        }

        [Fact]
        public void GetContacts_AddContacts()
        {
            // Arrange
            var contacts = GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(_service.AddContact(person));
            }

            // Act
            var getAllResponse= _service.GetContacts();

            // Assert
            foreach (var person in addedPeople)
            {
                Assert.Contains(person, getAllResponse);
            }
        }

        [Fact]
        public void GetContacts()
        {
            // Arrange
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            CountryResponse country = _countriesService.AddCountry(countryRequest);

            PersonAddRequest? personRequest = new PersonAddRequest() { Name = "John Doe", CountryId = country.Id };
            PersonResponse? personAddResponse = _service.AddContact(personRequest);

            // Act

            List<PersonResponse> getAllRequest = _service.GetContacts();

            // Assert
            Assert.True(personAddResponse.Id != Guid.Empty && getAllRequest.Count > 0);
            Assert.Contains(personAddResponse, getAllRequest);
        }

        #endregion

        #region GetFilteredContacts

        // requirements: if search is empty and searchBy is "Name", it should return all contacts
        [Fact]
        public void GetFilteredContacts_EmptySearch()
        {
            // Arrange
            var contacts = GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(_service.AddContact(person));
            }

            // Act
            var getFilteredResponse = _service.GetFilteredContacts(nameof(Person.Name), "");

            // Assert
            foreach (var person in addedPeople)
            {
                Assert.Contains(person, getFilteredResponse);
            }
        }

        [Fact]
        public void GetFilteredContacts_PersonName()
        {
            // Arrange
            var contacts = GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(_service.AddContact(person));
            }

            // Act
            var getFilteredResponse = _service.GetFilteredContacts(nameof(Person.Name), "ja");

            // Assert
            foreach (var person in addedPeople)
            {
                if (person.Name == null) continue;
                if (person.Name.Contains("ja", StringComparison.OrdinalIgnoreCase))
                {
                    Assert.Contains(person, getFilteredResponse);
                }
            }
            Assert.Equal(2, getFilteredResponse.Count);
        }

        #endregion

        #region GetSortedContacts

        [Fact]
        public void GetSortedContacts_PersonName_Descending()
        {
            // Arrange
            var contacts = GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(_service.AddContact(person));
            }

            // Act
            var getSortedResponse = _service.GetSortedContacts(
                _service.GetContacts(), 
                nameof(Person.Name), 
                SortOrder.Descending);

            var sortedAddedPeople = addedPeople.OrderByDescending(p => p.Name).ToList();

            // Assert
            for (int i = 0; i < sortedAddedPeople.Count; i++)
            {
                Assert.Equal(sortedAddedPeople[i], getSortedResponse[i]);
            }
        }

        #endregion

        #region UpdateContact

        // Requirement: when person is null, it should throw ArgumentNullException
        [Fact]
        public void UpdateContact_NullPerson()
        {
            // Arrange
            // Assert
            Assert.Throws<ArgumentNullException>(() => {
                //Act
                _service.UpdateContact(null);
            });
        }

        // Requirement: when person id is invalid, it should throw ArgumentException
        [Fact]
        public void UpdateContact_InvalidPersonId()
        {
            // Arrange
            PersonUpdateRequest? request = new PersonUpdateRequest() {Id = Guid.NewGuid() };
            // Assert
            Assert.Throws<ArgumentException>(() => {
                //Act
                _service.UpdateContact(request);
            });
        }

        // Requirement: when person name is null, it should throw ArgumentException
        [Fact]
        public void UpdateContact_NullPersonName()
        {
            // Arrange
            PersonResponse addedPerson = _service.AddContact(GenerateDummyPerson());
            PersonUpdateRequest updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Name = null;

            // Assert
            Assert.Throws<ArgumentException>(() => {
                // Act
                _service.UpdateContact(updateRequest);
            });
        }

        [Fact]
        public void UpdateContact()
        {
            // Arrange
            PersonResponse addedPerson = _service.AddContact(GenerateDummyPerson());
            PersonUpdateRequest updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Address = "123 Main Street";

            // Act
            PersonResponse updatedResponse = _service.UpdateContact(updateRequest);
            PersonResponse? getResponse = _service.GetContact(updatedResponse.Id);

            // Assert
            Assert.Equal(getResponse, updatedResponse);
        }

        #endregion

        #region DeleteContact

        // requirements: if person id is invalid, it should return false
        [Fact]
        public void DeleteContact_InvalidId()
        {
            // Arrange

            // Act
            bool isDeleted = _service.DeleteContact(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }

        [Fact]
        public void DeleteContact()
        {
            // Arrange
            PersonResponse addedPerson = _service.AddContact(GenerateDummyPerson());

            // Act
            bool isDeleted = _service.DeleteContact(addedPerson.Id);

            // Assert
            Assert.True(isDeleted);
        }
        #endregion

        private CountryResponse AddDummyCountry()
        {
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            return _countriesService.AddCountry(countryRequest);
        }

        private List<PersonAddRequest> GenerateDummyContactList()
        {
            var country = AddDummyCountry();
            List<PersonAddRequest> contacts = [
                new PersonAddRequest() { Name = "John Doe", Email = "john@email.com", CountryId = country.Id },
                new PersonAddRequest() { Name = "Jane Doe", Email = "jane@email.com", CountryId = country.Id },
                new PersonAddRequest() { Name = "Jade Doe", Email = "jade@email.com", CountryId = country.Id }
            ];
            return contacts;
        }

        private PersonAddRequest GenerateDummyPerson()
        {
            var country = AddDummyCountry();
            return new PersonAddRequest() {
                Name = "John Doe", Email = "john@email.com", Gender = Gender.Male, CountryId = country.Id 
            };
        }
    }
}
