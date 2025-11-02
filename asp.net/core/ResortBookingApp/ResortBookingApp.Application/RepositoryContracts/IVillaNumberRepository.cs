using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Application.RepositoryContracts
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task UpdateAsync(VillaNumber villaNumber);
    }
}
