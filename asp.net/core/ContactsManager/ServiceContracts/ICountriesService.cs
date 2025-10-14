using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Country Entity
    /// Design note: passing and returning DTO objects, not Country  
    /// </summary>
    public interface ICountriesService
    {   
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);

        Task<List<CountryResponse>> GetAllCountries();

        Task<CountryResponse?> GetCountry(Guid? id);
    }
}
