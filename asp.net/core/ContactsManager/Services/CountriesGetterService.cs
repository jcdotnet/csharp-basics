using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesGetterService : ICountriesGetterService
    {
        private readonly ICountriesRepository _repository;

        public CountriesGetterService(ICountriesRepository repository) {
            _repository = repository;
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
