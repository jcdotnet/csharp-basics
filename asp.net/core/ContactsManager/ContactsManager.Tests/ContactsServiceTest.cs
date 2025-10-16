using AutoFixture;
using AutoFixture.Kernel;
using Azure.Core;
using Entities;
using EntityFrameworkCoreMock;
using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Tests
{
    public class ContactsServiceTest
    {
        private readonly IContactsService _service;
        private readonly ICountriesService _countriesService;

        // AutoFixture automates non-relevant test fixture by generating dummy values
        private readonly IFixture _fixture; 

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

            _countriesService   = new CountriesService(dbContext);
            _service            = new ContactsService(dbContext);
            _fixture            = new Fixture();

        }

        #region AddContact

        // Requirement: when person is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddContact_NullPerson()
        {
            // Arrange
            PersonAddRequest? person = null;

            // Act
            var func = async () =>
            {
                await _service.AddContact(person);
            };

            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        // Requirement: when person name is null, it should throw ArgumentException
        // Optional: same with email addresses, etc.
        [Fact]
        public async Task AddContact_NullPersonName()
        {
            // Arrange
            //PersonAddRequest? person = new PersonAddRequest() { Name = null };
            var person = _fixture.Build<PersonAddRequest>().With(temp => temp.Name, null as string).Create();

            // Act
            var func = async () =>
            {
                await _service.AddContact(person);
            };

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        // Requirement: when proper person details, it should insert it in the contacts
        [Fact]
        public async Task AddContact()
        {
            // Arrange
            PersonAddRequest? personRequest = await GenerateDummyPerson();

            // Act
            PersonResponse? personResponse= await _service.AddContact(personRequest);
            List<PersonResponse> listResponse = await _service.GetContacts();

            // Assert
            //Assert.True(personResponse.Id != Guid.Empty);
            personResponse.Id.Should().NotBe(Guid.Empty);   // fluent assertion
            //Assert.Contains(personResponse, listResponse);
            listResponse.Should().Contain(personResponse);  // fluent assertion
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
            //Assert.Null(personGetResponse);
            personGetResponse.Should().BeNull();
        }

        [Fact]
        public async Task GetContact()
        {
            // Arrange
            CountryResponse country = await AddDummyCountry();
            PersonAddRequest? personRequest = await GenerateDummyPerson(); 
            PersonResponse? personAddResponse = await _service.AddContact(personRequest);

            // Act
            PersonResponse? personGetResponse = await _service.GetContact(personAddResponse.Id);

            // Assert
            //Assert.Equal(personAddResponse, personGetResponse);
            personGetResponse.Should().Be(personAddResponse); // fluent
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
            //Assert.Empty(contactsList);
            contactsList.Should().BeEmpty();
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
            //foreach (var person in addedPeople)
            //{
            //    Assert.Contains(person, getAllResponse); 
            //}
            addedPeople.Should().BeEquivalentTo(getAllResponse);
        }

        [Fact]
        public async Task GetContacts()
        {
            // Arrange
            PersonAddRequest? personRequest = await GenerateDummyPerson();
            PersonResponse? personAddResponse = await _service.AddContact(personRequest);

            // Act
            List<PersonResponse> getAllRequest = await _service.GetContacts();

            // Assert
            //Assert.True(personAddResponse.Id != Guid.Empty && getAllRequest.Count > 0);
            //Assert.Contains(personAddResponse, getAllRequest);
            personAddResponse.Id.Should().NotBe(Guid.Empty);
            getAllRequest.Should().HaveCountGreaterThan(0);
            getAllRequest.Should().Contain(personAddResponse);
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
            //foreach (var person in addedPeople)
            //{
            //    Assert.Contains(person, getFilteredResponse);
            //}
            addedPeople.Should().BeEquivalentTo(getFilteredResponse);
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
            //foreach (var person in addedPeople)
            //{
            //    if (person.Name == null) continue;
            //    if (person.Name.Contains("ja", StringComparison.OrdinalIgnoreCase))
            //    {
            //        Assert.Contains(person, getFilteredResponse);
            //    }
            //}
            addedPeople.Should().Contain(p => p.Name.Contains("ja", StringComparison.OrdinalIgnoreCase));
            //Assert.Equal(2, getFilteredResponse.Count);
            getFilteredResponse.Count.Should().Be(2);
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

            //var sortedAddedPeople = addedPeople.OrderByDescending(p => p.Name).ToList();

            // Assert
            //for (int i = 0; i < sortedAddedPeople.Count; i++)
            //{
            //    Assert.Equal(sortedAddedPeople[i], getSortedResponse[i]);
            //}
            getSortedResponse.Should().BeInDescendingOrder(p => p.Name);
        }

        #endregion

        #region UpdateContact

        // Requirement: when person is null, it should throw ArgumentNullException
        [Fact]
        public async Task UpdateContact_NullPerson()
        {
            // Act
            var func = async () =>
            {
                await _service.UpdateContact(null);
            };
            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        // Requirement: when person id is invalid, it should throw ArgumentException
        [Fact]
        public async Task UpdateContact_InvalidPersonId()
        {
            // Arrange
            PersonUpdateRequest? request = new PersonUpdateRequest() {Id = Guid.NewGuid() };

            //Act
            var func = async () =>
            {
                await _service.UpdateContact(request);
            };
            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        // Requirement: when person name is null, it should throw ArgumentException
        [Fact]
        public async Task UpdateContact_NullPersonName()
        {
            // Arrange
            PersonResponse addedPerson = await _service.AddContact(await GenerateDummyPerson());
            PersonUpdateRequest updateRequest = addedPerson.ToPersonUpdateRequest();
            updateRequest.Name = null;

            // Act
            var func = async () =>
            {
                await _service.UpdateContact(updateRequest);
            };
            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
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
            //Assert.Equal(getResponse, updatedResponse);
            updatedResponse.Should().Be(getResponse);
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
            //Assert.False(isDeleted);
            isDeleted.Should().BeFalse();
        }

        [Fact]
        public async Task DeleteContact()
        {
            // Arrange
            PersonResponse addedPerson = await _service.AddContact(await GenerateDummyPerson());

            // Act
            bool isDeleted = await _service.DeleteContact(addedPerson.Id);

            // Assert
            //Assert.True(isDeleted);
            isDeleted.Should().BeTrue();
        }
        #endregion

        private async Task<CountryResponse> AddDummyCountry()
        {
            CountryAddRequest countryRequest = _fixture.Create<CountryAddRequest>();
            return await _countriesService.AddCountry(countryRequest);
        }

        private async Task<List<PersonAddRequest>> GenerateDummyContactList()
        {
            var country = await AddDummyCountry();
            List<PersonAddRequest> contacts = [
                 _fixture.Build<PersonAddRequest>().With(temp => temp.Name, "John Doe")
                .With(temp => temp.Email, "john@email.com").With(temp => temp.CountryId, country.Id).Create(),
                _fixture.Build<PersonAddRequest>().With(temp => temp.Name, "Jane Doe")
                .With(temp => temp.Email, "jane@email.com").With(temp => temp.CountryId, country.Id).Create(),
                _fixture.Build<PersonAddRequest>().With(temp => temp.Name, "Jade Doe")
                .With(temp => temp.Email, "jade@email.com").With(temp => temp.CountryId, country.Id).Create(),
            ];
            return contacts;
        }

        private async Task<PersonAddRequest> GenerateDummyPerson()
        {
            var country = await AddDummyCountry();

            //return new PersonAddRequest() {
            //    Name = "John Doe", Email = "john@email.com", Gender = Gender.Male, CountryId = country.Id 
            //};
            //return _fixture.Create<PersonAddRequest>(); // initializes all model properties with dummy values
            return _fixture.Build<PersonAddRequest>()  // build allows us to customize the dummy values
                .With(temp => temp.Email, "email@example.com")
                .With(temp => temp.CountryId, country.Id)
                .Create();
        }
    }
}
