using AutoFixture;
using ContactsManager.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Tests
{
    public class ContactsControllerTest
    {
        private readonly IContactsService _contactsService;
        private readonly ICountriesService _countriesService;

        private readonly Mock<IContactsService> _contactsServiceMock;
        private readonly Mock<ICountriesService> _countriesServiceMock;

        private readonly Fixture _fixture;

        public ContactsControllerTest()
        {
            _fixture = new Fixture();
            _contactsServiceMock = new Mock<IContactsService>();
            _countriesServiceMock = new Mock<ICountriesService>();

            _contactsService = _contactsServiceMock.Object;
            _countriesService = _countriesServiceMock.Object;
        }

        #region Index

        [Fact]
        public async Task Index_ShouldReturnIndexViewWithContactsLists()
        {
            // Arrange
            var contactList = _fixture.Create<List<PersonResponse>>();
            var controller = new ContactsController(_countriesService, _contactsService);

            _contactsServiceMock.Setup(temp => temp.GetFilteredContacts(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(contactList);

            _contactsServiceMock
                .Setup(temp => temp.GetSortedContacts(
                    It.IsAny<List<PersonResponse>>(), It.IsAny<string>(), It.IsAny<SortOrder>())
                )
                .ReturnsAsync(contactList);

            // Act
            IActionResult actionResult = await controller.Index(
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<string>(),
                _fixture.Create<SortOrder>());

            // Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

            viewResult.ViewData.Model.Should().BeAssignableTo<IEnumerable<PersonResponse>>();
            viewResult.ViewData.Model.Should().Be(contactList);
        }

        #endregion

        #region Create

        [Fact]
        public async Task Create_ModelErrors_ToReturnCreateView()
        {
            //Arrange
            PersonAddRequest personAddRequest = _fixture.Create<PersonAddRequest>();
            PersonResponse personResponse = _fixture.Create<PersonResponse>();
           
            List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

            _countriesServiceMock.Setup(temp => temp.GetCountries()).ReturnsAsync(countries);
            _contactsServiceMock.Setup(temp => temp.AddContact(It.IsAny<PersonAddRequest>()))
             .ReturnsAsync(personResponse);

            var controller = new ContactsController(_countriesService, _contactsService);

            //Act
            controller.ModelState.AddModelError("Name", "Person Name is required");

            IActionResult actionResult = await controller.Create(personAddRequest);

            //Assert
            ViewResult viewResult = Assert.IsType<ViewResult>(actionResult);

            viewResult.ViewData.Model.Should().BeAssignableTo<PersonAddRequest>();
            viewResult.ViewData.Model.Should().Be(personAddRequest);
        }


        [Fact]
        public async Task Create_NoModelErrors_ToRedirectToIndex()
        {
            //Arrange
            PersonAddRequest personAddRequest = _fixture.Create<PersonAddRequest>();
            PersonResponse personResponse = _fixture.Create<PersonResponse>();
            List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

            _countriesServiceMock.Setup(temp => temp.GetCountries()).ReturnsAsync(countries);
            _contactsServiceMock
             .Setup(temp => temp.AddContact(It.IsAny<PersonAddRequest>()))
             .ReturnsAsync(personResponse);

            var controller = new ContactsController(_countriesService, _contactsService);

            //Act
            IActionResult actionResult = await controller.Create(personAddRequest);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(actionResult);

            redirectResult.ActionName.Should().Be("Index");
        }

        #endregion
    }
}
