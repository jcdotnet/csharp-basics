using ContactsManager.Core.Entities;

namespace RepositoryContracts
{
    public interface ICountriesRepository
    {
        Task<Country> AddCountry(Country country);

        Task<List<Country>> GetCountries();

        Task<Country?> GetCountry(Guid id);

        Task<Country?> GetCountryByName(string name);
    }
}
