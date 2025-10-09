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
    }
}
