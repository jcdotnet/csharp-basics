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
            _service = new CountriesService();
        }

        // Requirement: when country is null, it should throw ArgumentNullException
        [Fact]
        public void AddCountry_NullCountry()
        {
            // Arrange
            CountryAddRequest? request = null;

            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                // Act
                _service.AddCountry(request);
            });
        }

        // Requirement: when country name is null, it should throw ArgumentException
        [Fact]
        public void AddCountry_NullCountryName()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { Name = null };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.AddCountry(request);
            });
        }

        // Requirement: when country name is duplicate, it should throw ArgumentException
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            // Arrange
            CountryAddRequest? request1 = new CountryAddRequest() { Name = "Spain" };
            CountryAddRequest? request2 = new CountryAddRequest() { Name = "Spain" };

            // Assert
            Assert.Throws<ArgumentException>(() =>
            {
                // Act
                _service.AddCountry(request1);
                _service.AddCountry(request2);
            });
        }

        // Requirement: when proper country name, it should insert it in the list of countries
        [Fact]
        public void AddCountry()
        {
            // Arrange
            CountryAddRequest? request = new CountryAddRequest() { Name = "Spain" };

            // Act
            CountryResponse? response = _service.AddCountry(request);

            // Assert
            Assert.True(response.Id != Guid.Empty);
        }
    }
}
