using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ICountriesRepository _repository;

        public CountriesService(ICountriesRepository repository) {
            _repository = repository;
        }

        public async Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest)
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

            if (await _repository.GetCountryByName(countryAddRequest.Name) != null)
            {
                throw new ArgumentException("Country already exists");
            }
            // end validations

            Country country = countryAddRequest.ToCountry();
            country.Id = Guid.NewGuid();
            await _repository.AddCountry(country);

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetCountries()
        {
            return (await _repository.GetCountries()).Select(c => c.ToCountryResponse()).ToList();
        }

        public async Task<CountryResponse?> GetCountry(Guid? id)
        {
            if (id == null) return null;

            return (await _repository.GetCountry(id.Value))?.ToCountryResponse();
        }
    }
}
