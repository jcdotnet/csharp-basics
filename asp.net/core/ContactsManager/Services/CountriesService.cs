using Entities;
using Microsoft.EntityFrameworkCore;
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

            if (await _db.Countries.CountAsync(c => c.Name == countryAddRequest.Name) > 0)
            {
                throw new ArgumentException("Country already exists");
            }
            // end validations

            Country country = countryAddRequest.ToCountry();
            country.Id = Guid.NewGuid();
            _db.Countries.Add(country);
            await _db.SaveChangesAsync(); // DDL

            return country.ToCountryResponse();
        }

        public async Task<List<CountryResponse>> GetAllCountries()
        {
            return await _db.Countries.Select(c => c.ToCountryResponse()).ToListAsync();
        }

        public async Task<CountryResponse?> GetCountry(Guid? id)
        {
            if (id == null) return null;

            return (await _db.Countries.FirstOrDefaultAsync(c => c.Id == id))?.ToCountryResponse();
        }
    }
}
