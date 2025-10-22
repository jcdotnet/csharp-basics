using ContactsManager.Core.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Country Entity
    /// Design note: passing and returning DTO objects, not Country  
    /// </summary>
    public interface ICountriesAdderService
    {   
        Task<CountryResponse> AddCountry(CountryAddRequest? countryAddRequest);
    }
}
