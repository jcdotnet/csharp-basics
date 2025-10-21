using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Country Entity
    /// Design note: passing and returning DTO objects, not Country  
    /// </summary>
    public interface ICountriesGetterService
    {   
        Task<List<CountryResponse>> GetCountries();

        Task<CountryResponse?> GetCountry(Guid? id);
    }
}
