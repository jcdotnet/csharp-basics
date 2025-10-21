using ContactsManager.Domain.Entities;
using ContactsManager.DTO;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class CountriesAdderService : ICountriesAdderService
    {
        private readonly ICountriesRepository _repository;

        public CountriesAdderService(ICountriesRepository repository) {
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
    }
}
