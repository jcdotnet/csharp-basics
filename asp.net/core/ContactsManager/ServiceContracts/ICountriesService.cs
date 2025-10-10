using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Logic business for manipulating the Country Entity
    /// Design note: passing and returning DTO objects, not Country  
    /// </summary>
    public interface ICountriesService
    {   
        CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        List<CountryResponse> GetAllCountries();

        CountryResponse? GetCountry(Guid? id);
    }
}
