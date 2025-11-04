using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        Task UpdateAsync(Amenity amenity);
    }
}
