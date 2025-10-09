using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private List<Country> _countries;

        public CountriesService() { 
            _countries = new List<Country>();
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
