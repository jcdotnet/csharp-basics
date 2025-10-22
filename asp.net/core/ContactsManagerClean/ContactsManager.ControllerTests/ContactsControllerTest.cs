using AutoFixture;
using ContactsManager.Core.DTO;
using ContactsManager.Web.Controllers;
using ContactsManager.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ServiceContracts;

namespace ContactsManager.ControllerTests
{
    public class ContactsControllerTest
    {
        private readonly IContactsAdderService _contactsAdderService;
        private readonly IContactsGetterService _contactsGetterService;
        private readonly IContactsUpdaterService _contactsUpdaterService;
        private readonly IContactsDeleterService _contactsDeleterService;
        private readonly IContactsSorterService _contactsSorterService;
        private readonly ICountriesGetterService _countriesService;

        private readonly Mock<IContactsAdderService> _contactsAdderServiceMock;
        private readonly Mock<IContactsGetterService> _contactsGetterServiceMock;
        private readonly Mock<IContactsUpdaterService> _contactsUpdaterServiceMock;
        private readonly Mock<IContactsDeleterService> _contactsDeleterServiceMock;
        private readonly Mock<IContactsSorterService> _contactsSorterServiceMock;
        private readonly Mock<ICountriesGetterService> _countriesServiceMock;

        private readonly Fixture _fixture;

        public ContactsControllerTest()
        {
            _fixture = new Fixture();
            _contactsAdderServiceMock = new Mock<IContactsAdderService>();
            _contactsGetterServiceMock = new Mock<IContactsGetterService>();
            _contactsUpdaterServiceMock = new Mock<IContactsUpdaterService>();
            _contactsDeleterServiceMock = new Mock<IContactsDeleterService>();
            _contactsSorterServiceMock = new Mock<IContactsSorterService>();
            _countriesServiceMock = new Mock<ICountriesGetterService>();

            _contactsAdderService = _contactsAdderServiceMock.Object;
            _contactsGetterService = _contactsGetterServiceMock.Object;
            _contactsSorterService = _contactsSorterServiceMock.Object;
            _contactsUpdaterService = _contactsUpdaterServiceMock.Object;
            _contactsDeleterService = _contactsDeleterServiceMock.Object;
            _countriesService = _countriesServiceMock.Object;
        }

        #region Index

        [Fact]
        public async Task Index_ShouldReturnIndexViewWithContactsLists()
        {
            // Arrange
            var contactList = _fixture.Create<List<PersonResponse>>();
            var controller = new ContactsController(_contactsAdderService, _contactsGetterService, 
                _contactsUpdaterService, _contactsSorterService, _contactsDeleterService, _countriesService);

            _contactsGetterServiceMock.Setup(temp => temp.GetFilteredContacts(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(contactList);

            _contactsSorterServiceMock
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

        // now, the filter is the responsible of checking the model state, not the controller
        //[Fact]
        //public async Task Create_ModelErrors_ToReturnCreateView()
        //{
        //   // test not applicabe
        //}
        
        [Fact]
        public async Task Create_NoModelErrors_ToRedirectToIndex()
        {
            //Arrange
            PersonAddRequest personAddRequest = _fixture.Create<PersonAddRequest>();
            PersonResponse personResponse = _fixture.Create<PersonResponse>();
            List<CountryResponse> countries = _fixture.Create<List<CountryResponse>>();

            _countriesServiceMock.Setup(temp => temp.GetCountries()).ReturnsAsync(countries);
            _contactsAdderServiceMock
             .Setup(temp => temp.AddContact(It.IsAny<PersonAddRequest>()))
             .ReturnsAsync(personResponse);

            var controller = new ContactsController(_contactsAdderService, _contactsGetterService,
                 _contactsUpdaterService, _contactsSorterService, _contactsDeleterService, _countriesService);

            //Act
            IActionResult actionResult = await controller.Create(personAddRequest);

            //Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(actionResult);

            redirectResult.ActionName.Should().Be("Index");
        }

        #endregion
    }
}
