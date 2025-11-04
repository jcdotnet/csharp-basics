using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task UpdateAsync(VillaNumber villaNumber);
    }
}
