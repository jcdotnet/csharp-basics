using Entities;
using Microsoft.EntityFrameworkCore;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace ContactsManager.Tests
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _service;

        public CountriesServiceTest()
        {
            _service = new CountriesService(new ContactsManagerDbContext(new DbContextOptionsBuilder<ContactsManagerDbContext>().Options));
        }

        #region AddCountry
        // Requirement: when country is null, it should throw ArgumentNullException
        [Fact]
        public async Task AddCountry_NullCountry() 
        {
            // Arrange
            CountryAddRequest? request = null;

            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                // Act
                await _service.AddCountry(request);
            });
        }

        // Requirement: when country name is null, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_NullCountryName()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { Name = null };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                // Act
                await _service.AddCountry(request);
            });
        }

        // Requirement: when country name is duplicate, it should throw ArgumentException
        [Fact]
        public async Task AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { Name = "Spain" };
            CountryAddRequest? request2 = new CountryAddRequest() { Name = "Spain" };

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async() =>
            {
                // Act
                await _service.AddCountry(request1);
                await _service.AddCountry(request2);
            });
        }

        // Requirement: when proper country name, it should insert it in the list of countries
        [Fact]
        public async Task AddCountry()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { Name = "Spain" };

            // Act
            CountryResponse? response = await _service.AddCountry(request);

            // Assert
            Assert.True(response.Id != Guid.Empty);
        }
        #endregion

        #region GetAllCountries

        // requirement: the list of countries should be emtpy by default
        [Fact]
        public async Task GetAllCountries_DefaulEmptytList()
        {
            // Arrange
            // Act
            var response = await _service.GetAllCountries();

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public async Task GetAllCountries_AddCountries()
        {
            // Arrange
            var request = new List<CountryAddRequest>() {
                new CountryAddRequest() { Name = "Spain"},
                new CountryAddRequest() { Name = "USA" }
            };

            // Act
            var list = new List<CountryResponse>();
            foreach (var country in request)
            {
                list.Add(await _service.AddCountry(country));            
            }
            var responseList = await _service.GetAllCountries();

            // Assert
            foreach (var country in list)
            {
                Assert.Contains(country, responseList);
            }
        }

        [Fact]
        public async Task GetAllCountries()
        {
            // Arrange
            CountryAddRequest request = new CountryAddRequest() { Name = "Spain" };

            // Act
            var response = await _service.AddCountry(request);
            var responseList = await _service.GetAllCountries();

            // Assert
            Assert.True(response.Id != Guid.Empty && responseList.Count > 0);
            Assert.Contains(response, responseList);
        }
        #endregion

        #region GetCountry

        [Fact]
        public async Task GetCountry_NullCountryId()
        {
            // Arrange
            Guid? id = null;

            // Act
            var response = await _service.GetCountry(id);

            // Assert
            Assert.Null(response);
        }

        [Fact]
        public async Task GetCountry()
        {
            // Arrange
            var request = new CountryAddRequest() { Name = "Spain" };
            var addResponse = await _service.AddCountry(request);

            // Act
            var getReponse = await _service.GetCountry(addResponse.Id);

            // Assert
            Assert.Equal(addResponse, getReponse);
        }
        #endregion
    }
}