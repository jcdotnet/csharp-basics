using ContactsManager.Domain.Entities;

namespace ContactsManager.DTO
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
