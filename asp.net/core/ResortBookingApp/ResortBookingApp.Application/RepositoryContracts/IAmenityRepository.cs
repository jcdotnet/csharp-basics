using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Application.RepositoryContracts
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        Task UpdateAsync(Amenity amenity);
    }
}
