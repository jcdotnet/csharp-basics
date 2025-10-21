using AutoFixture;
using Entities;
using FluentAssertions;
using Moq;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ContactsManager.Tests
{
    public class CountriesServiceTest
    {      
        private readonly ICountriesAdderService _adderService;
        private readonly ICountriesGetterService _getterService;
        private readonly Mock<ICountriesRepository> _repositoryMock;
        private readonly ICountriesRepository _repository;

        // AutoFixture automates non-relevant test fixture by generating dummy values
        private readonly IFixture _fixture;

        public CountriesServiceTest()
        {
            _repositoryMock = new Mock<ICountriesRepository>();
            _repository     = _repositoryMock.Object;
            _adderService   = new CountriesAdderService(_repository);
            _getterService  = new CountriesGetterService(_repository);
            _fixture        = new Fixture();
        }

        #region AddCountry

        // Requirement: when country is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry_ThrowsArgumentNullException() 
        {
            // Arrange
            CountryAddRequest? request = null;

            // Act
            var func = async () =>
            {
                await _adderService.AddCountry(request);
            };
            // Assert
            await func.Should().ThrowAsync<ArgumentNullException>();
        }

        // Requirement: when country name is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_NullCountryName_ThrowsArgumentException()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { Name = null };
            Country country = request.ToCountry();

            // mocking the repository (using test double method instead of the GetContacts method)
            // i.e. creating an object that look and behave like their production equivalent ones
            _repositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>())).ReturnsAsync(country);

            // Act
            var func = async () =>
            {
                await _adderService.AddCountry(request);
            };

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        // Requirement: when country name is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateCountryName_ThrowsArgumentException()
        {
            // Arrange
            CountryAddRequest request1 = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.Name, "Spain").Create();
            Country country1 = request1.ToCountry();
            CountryAddRequest request2 = _fixture.Build<CountryAddRequest>()
                .With(temp => temp.Name, "Spain").Create();
            Country country2 = request2.ToCountry();

            _repositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>()))
                .ReturnsAsync(country1);
            _repositoryMock.Setup(temp => temp.GetCountryByName(It.IsAny<string>()))
                .ReturnsAsync(null as Country);
            
            // Act
            await _adderService.AddCountry(request1);
            var func = async () =>
            {
                _repositoryMock.Setup(temp => temp.AddCountry(It.IsAny<Country>()))
                    .ReturnsAsync(country1);
                _repositoryMock.Setup(temp => temp.GetCountryByName(It.IsAny<string>()))
                    .ReturnsAsync(country1);
                await _adderService.AddCountry(request2);
            };

            // Assert
            await func.Should().ThrowAsync<ArgumentException>();
        }

        // Requirement: when proper country name, it should insert it in the list of countries
        [Fact]
        public async Task AddCountry_ToBeSuccessful()
        {
            // Arrange
            CountryAddRequest request = _fixture.Create<CountryAddRequest>();

            // Act
            CountryResponse? response = await _adderService.AddCountry(request);

            // Assert
            Assert.True(response.Id != Guid.Empty);
        }
        #endregion

        #region GetAllCountries

        // requirement: the list of countries should be emtpy by default
        [Fact]
        public async Task GetCountries_ToBeEmptytList()
        {
            // Arrange
            List<Country> emptyList = [];
            _repositoryMock.Setup(temp => temp.GetCountries()).ReturnsAsync(emptyList);

            // Act
            var getResponse = await _getterService.GetCountries();

            // Assert
            getResponse.Should().BeEmpty();
        }

        [Fact]
        public async Task GetCountries_AddCountries_ToBeSuccessful()
        {
            // Arrange
            var expectedList = new List<Country>() {
                 _fixture.Create<Country>(),
                 _fixture.Create<Country>(),
            };

            _repositoryMock.Setup(temp => temp.GetCountries()).ReturnsAsync(expectedList);

            // Act
            var responseList = await _getterService.GetCountries();

            // Assert
            responseList.Should().BeEquivalentTo(expectedList);
        }

        #endregion

        #region GetCountry

        [Fact]
        public async Task GetCountry_NullCountryId_ToBeNull()
        {
            // Arrange
            Guid? id = null;

            _repositoryMock.Setup(temp => temp.GetCountry(It.IsAny<Guid>()))
                .ReturnsAsync(null as Country);

            // Act
            var response = await _getterService.GetCountry(id);

            // Assert
            response.Should().BeNull();
        }

        [Fact]
        public async Task GetCountry_ValidCountryId_ToBeSuccessful()
        {
            // Arrange
            var country = _fixture.Create<Country>();
            _repositoryMock.Setup(temp => temp.GetCountry(It.IsAny<Guid>()))
                .ReturnsAsync(country);

            // Act
            var getReponse = await _getterService.GetCountry(country.Id);

            // Assert
            getReponse.Should().Be(country.ToCountryResponse());
        }

        #endregion
    }
}