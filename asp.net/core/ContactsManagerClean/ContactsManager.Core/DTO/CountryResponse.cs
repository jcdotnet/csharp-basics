using ContactsManager.Core.Entities;

namespace ContactsManager.Core.DTO
{
    public class CountryResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(CountryResponse)) return false;
            return this.Id == ((CountryResponse)obj).Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        public static CountryResponse ToCountryResponse(this Country country)
        {
            return new CountryResponse { Id = country.Id, Name = country.Name };
        }
    }
}
