using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories
{
    public class CountriesRepository : ICountriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CountriesRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Country> AddCountry(Country country)
        {
            // repository does not check anything, it assumes that someone else's did
            // in this case, the service takes care of the validation / business logic
            // so country here is not null, the country name is not duplicated, etc
            _db.Countries.Add(country);
            await _db.SaveChangesAsync();
            return country;
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _db.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountry(Guid id)
        {
            return await _db.Countries.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Country?> GetCountryByName(string name)
        {
            return await _db.Countries.FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
