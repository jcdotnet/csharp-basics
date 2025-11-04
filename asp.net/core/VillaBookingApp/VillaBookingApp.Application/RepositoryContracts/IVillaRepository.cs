using VillaBookingApp.Domain.Entities;

namespace VillaBookingApp.Application.RepositoryContracts
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task UpdateAsync(Villa villa);

    }
}
