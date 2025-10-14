using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        private readonly ContactsManagerDbContext _db;

        public CountriesService(ContactsManagerDbContext dbContext) {
            _db = dbContext;
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

            if (_db.Countries.Count(c => c.Name == countryAddRequest.Name) > 0)
            {
                throw new ArgumentException("Country already exists");
            }
            // end validations

            Country country = countryAddRequest.ToCountry();
            country.Id = Guid.NewGuid();
            _db.Countries.Add(country);
            _db.SaveChanges(); // DDL

            return country.ToCountryResponse();
        }

        public List<CountryResponse> GetAllCountries()
        {
            return _db.Countries.Select(c => c.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountry(Guid? id)
        {
            if (id == null) return null;

            return _db.Countries.FirstOrDefault(c => c.Id == id)?.ToCountryResponse();
        }
    }
}
