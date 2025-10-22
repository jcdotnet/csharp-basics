using ContactsManager.Core.Entities;

namespace ContactsManager.Core.DTO
{
    public class CountryAddRequest
    {
        public string? Name { get; set; }

        public Country ToCountry()
        {
            return new Country() { Name = Name };
        }
    }
}
