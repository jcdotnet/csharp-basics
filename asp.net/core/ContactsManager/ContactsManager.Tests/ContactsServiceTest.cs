using Entities;
using EntityFrameworkCoreMock;
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
            // mocking the dbContext (using test double instead of DbContext)
            // object that look and behave like their production equivalent ones 
            var countriesInitialData    = new List<Country>();  // empty collection as a data source
            var peopleInitialData       = new List<Person>();   // empty collection as a data source
            DbContextMock<ApplicationDbContext> dbContextMock = new DbContextMock<ApplicationDbContext>(
                new DbContextOptionsBuilder<ApplicationDbContext>().Options
            );
            ApplicationDbContext dbContext = dbContextMock.Object;

            // mocking the dbSets
            dbContextMock.CreateDbSetMock(temp => temp.Countries, countriesInitialData);
            dbContextMock.CreateDbSetMock(temp => temp.People, peopleInitialData);

            _countriesService = new CountriesService(dbContext);
            _service = new ContactsService(dbContext);
        }

        #region AddContact

        // Requirement: when person is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddContact_NullPerson()
        {
            // Arrange
            PersonAddRequest? person = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _service.AddContact(person);
            });
        }

        // Requirement: when person name is null, it should throw ArgumentException
        // Optional: same with email addresses, etc.
        [Fact]
        public async Task AddContact_NullPersonName()
        {
            // Arrange
            PersonAddRequest? person = new PersonAddRequest() { Name = null };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () =>
            {
                // Act
                await _service.AddContact(person);
            });
        }

        // Requirement: when proper person details, it should insert it in the contacts
        [Fact]
        public async Task AddContact()
        {
            // Arrange
            PersonAddRequest? personRequest = new PersonAddRequest() { Name = "John Doe" };

            // Act
            PersonResponse? personResponse= await _service.AddContact(personRequest);
            List<PersonResponse> listResponse = await _service.GetContacts();

            // Assert
            Assert.True(personResponse.Id != Guid.Empty);
            Assert.Contains(personResponse, listResponse);
        }

        #endregion

        #region GetContact

        // Requirement: when person id is null, it should return null
        [Fact]
        public async Task GetContact_NullPersonId()
        {
            // Arrange
            // Act
            PersonResponse? personGetResponse = await _service.GetContact(null);

            // Assert
            Assert.Null(personGetResponse);
        }

        [Fact]
        public async Task GetContact()
        {
            // Arrange
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            CountryResponse country = await _countriesService.AddCountry(countryRequest);

            PersonAddRequest? personRequest = new PersonAddRequest() { Name = "John Doe", CountryId = country.Id};
            PersonResponse? personAddResponse = await _service.AddContact(personRequest);

            // Act
            PersonResponse? personGetResponse = await _service.GetContact(personAddResponse.Id);

            // Assert
            Assert.Equal(personAddResponse, personGetResponse);
        }

        #endregion

        #region GetContacts

        // requirement: the contact list should be emtpy by default
        [Fact]
        public async Task GetContacts_DefaulEmptytList()
        {
            // Arrange
            // Act
            var contactsList = await _service.GetContacts();

            // Assert
            Assert.Empty(contactsList);
        }

        [Fact]
        public async Task GetContacts_AddContacts()
        {
            // Arrange
            var contacts = await GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(await _service.AddContact(person));
            }

            // Act
            var getAllResponse= await _service.GetContacts();

            // Assert
            foreach (var person in addedPeople)
            {
                Assert.Contains(person, getAllResponse);
            }
        }

        [Fact]
        public async Task GetContacts()
        {
            // Arrange
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            CountryResponse country = await _countriesService.AddCountry(countryRequest);

            PersonAddRequest? personRequest = new PersonAddRequest() { Name = "John Doe", CountryId = country.Id };
            PersonResponse? personAddResponse = await _service.AddContact(personRequest);

            // Act
            List<PersonResponse> getAllRequest = await _service.GetContacts();

            // Assert
            Assert.True(personAddResponse.Id != Guid.Empty && getAllRequest.Count > 0);
            Assert.Contains(personAddResponse, getAllRequest);
        }

        #endregion

        #region GetFilteredContacts

        // requirements: if search is empty and searchBy is "Name", it should return all contacts
        [Fact]
        public async Task GetFilteredContacts_EmptySearch()
        {
            // Arrange
            var contacts = await GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(await _service.AddContact(person));
            }

            // Act
            var getFilteredResponse = await _service.GetFilteredContacts(nameof(Person.Name), "");

            // Assert
            foreach (var person in addedPeople)
            {
                Assert.Contains(person, getFilteredResponse);
            }
        }

        [Fact]
        public async Task GetFilteredContacts_PersonName()
        {
            // Arrange
            var contacts = await GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(await _service.AddContact(person));
            }

            // Act
            var getFilteredResponse = await _service.GetFilteredContacts(nameof(Person.Name), "ja");

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
        public async Task GetSortedContacts_PersonName_Descending()
        {
            // Arrange
            var contacts = await GenerateDummyContactList();

            var addedPeople = new List<PersonResponse>();
            foreach (var person in contacts)
            {
                addedPeople.Add(await _service.AddContact(person));
            }

            // Act
            var getSortedResponse = await _service.GetSortedContacts(
                await _service.GetContacts(), 
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
        public async Task UpdateContact_NullPerson()
        {
            // Arrange
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () => {
                //Act
                await _service.UpdateContact(null);
            });
        }

        // Requirement: when person id is invalid, it should throw ArgumentException
        [Fact]
        public async Task UpdateContact_InvalidPersonId()
        {
            // Arrange
            PersonUpdateRequest? request = new PersonUpdateRequest() {Id = Guid.NewGuid() };
            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => {
                //Act
                await _service.UpdateContact(request);
            });
        }

        // Requirement: when person name is null, it should throw ArgumentException
        [Fact]
        public async Task UpdateContact_NullPersonName()
        {
            // Arrange
            PersonResponse addedPerson = await _service.AddContact(await GenerateDummyPerson());
            PersonUpdateRequest updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Name = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async() => {
                // Act
                await _service.UpdateContact(updateRequest);
            });
        }

        [Fact]
        public async Task UpdateContact()
        {
            // Arrange
            PersonResponse addedPerson = await _service.AddContact(await GenerateDummyPerson());
            PersonUpdateRequest updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Address = "123 Main Street";

            // Act
            PersonResponse updatedResponse = await _service.UpdateContact(updateRequest);
            PersonResponse? getResponse = await _service.GetContact(updatedResponse.Id);

            // Assert
            Assert.Equal(getResponse, updatedResponse);
        }

        #endregion

        #region DeleteContact

        // requirements: if person id is invalid, it should return false
        [Fact]
        public async Task DeleteContact_InvalidId()
        {
            // Arrange
            // Act
            bool isDeleted = await _service.DeleteContact(Guid.NewGuid());

            // Assert
            Assert.False(isDeleted);
        }

        [Fact]
        public async Task DeleteContact()
        {
            // Arrange
            PersonResponse addedPerson = await _service.AddContact(await GenerateDummyPerson());

            // Act
            bool isDeleted = await _service.DeleteContact(addedPerson.Id);

            // Assert
            Assert.True(isDeleted);
        }
        #endregion

        private async Task<CountryResponse> AddDummyCountry()
        {
            CountryAddRequest countryRequest = new CountryAddRequest() { Name = "Spain" };
            return await _countriesService.AddCountry(countryRequest);
        }

        private async Task<List<PersonAddRequest>> GenerateDummyContactList()
        {
            var country = await AddDummyCountry();
            List<PersonAddRequest> contacts = [
                new PersonAddRequest() { Name = "John Doe", Email = "john@email.com", CountryId = country.Id },
                new PersonAddRequest() { Name = "Jane Doe", Email = "jane@email.com", CountryId = country.Id },
                new PersonAddRequest() { Name = "Jade Doe", Email = "jade@email.com", CountryId = country.Id }
            ];
            return contacts;
        }

        private async Task<PersonAddRequest> GenerateDummyPerson()
        {
            var country = await AddDummyCountry();
            return new PersonAddRequest() {
                Name = "John Doe", Email = "john@email.com", Gender = Gender.Male, CountryId = country.Id 
            };
        }
    }
}
