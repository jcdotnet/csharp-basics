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

        #region AddCountry
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
        #endregion

        #region GetAllCountries
        // requirement: the list of countries should be emtpy by default
        [Fact]
        public void GetAllCountries_DefaulEmptytList()
        {
            // Arrange
            // Act
            var response = _service.GetAllCountries();

            // Assert
            Assert.Empty(response);
        }

        [Fact]
        public void GetAllCountries_AddCountries()
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
                list.Add(_service.AddCountry(country));            
            }
            var responseList = _service.GetAllCountries();

            // Assert
            foreach (var country in list)
            {
                Assert.Contains(country, responseList);
            }
        }

        [Fact]
        public void GetAllCountries()
        {
            // Arrange
            CountryAddRequest request = new CountryAddRequest() { Name = "Spain" };

            // Act
            var response = _service.AddCountry(request);
            var responseList = _service.GetAllCountries();

            // Assert
            Assert.True(response.Id != Guid.Empty && responseList.Count > 0);
            Assert.Contains(response, responseList);
        }
        #endregion
    }
}
