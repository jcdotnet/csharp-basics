using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
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
            _service = new ContactsService();
            _countriesService = new CountriesService();
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
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            CountryResponse country = _countriesService.AddCountry(countryRequest);
            var contacts = new List<PersonAddRequest>() {
                new PersonAddRequest() { Name = "John Doe", CountryId = country.Id },
                new PersonAddRequest() { Name = "Jane Doe", CountryId = country.Id }
            };

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
    }
}
