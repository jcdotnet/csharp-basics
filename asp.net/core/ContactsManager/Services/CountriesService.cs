using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private List<Country> _countries;

        public CountriesService(bool initializeWithMockData = true) {
            _countries = new List<Country>();
            if (initializeWithMockData)
            {
                _countries.AddRange([
                    new Country() { Id = Guid.Parse("6B820C12-FF44-4412-9E14-7941A869FB03"), Name = "USA" },
                    new Country() { Id = Guid.Parse("D736FB55-4204-41DA-B3DA-144B4C70DD51"), Name = "UK" },
                    new Country() { Id = Guid.Parse("59C3A91A-E333-409D-8D95-C6E42CCD1680"), Name = "Spain" },
                    new Country() { Id = Guid.Parse("28DC009E-FC31-4598-A5A5-692F3E84E0CD"), Name = "Germany" },
                    new Country() { Id = Guid.Parse("FFD79E28-72C6-4181-A5EB-F09B02252CD6"), Name = "Canada" }
                ]);
            }
        }

        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            // validations
            if (countryAddRequest == null)
            {
                throw new ArgumentNullException(nameof(countryAddRequest));
            }

            if (countryAddRequest.Name == null)
            {
                throw new ArgumentException(nameof(countryAddRequest.Name));
            }

            if (_countries.Where(c => c.Name == countryAddRequest.Name).Count() > 0)
            {
                throw new ArgumentException("Country already exists");
            }
            // end validations

            Country country = countryAddRequest.ToCountry();
            country.Id = Guid.NewGuid();
            _countries.Add(country);

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountry(Guid? id)
        {
            if (id == null) return null;

            return _countries.FirstOrDefault(c => c.Id == id)?.ToCountryResponse();
        }
    }
}
