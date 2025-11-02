using ResortBookingApp.Domain.Entities;

namespace ResortBookingApp.Application.RepositoryContracts
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task UpdateAsync(Villa villa);

    }
}
